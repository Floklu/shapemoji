using System.Collections.Generic;
using Harpoon;
using Spawner;
using UnityEngine;

/**
 * HookableObjectController is a static class and contains the functionality of Hookable Objects 
 */
public static class HookableObjectController
{
    //list of receivers
    private static readonly List<HarpoonController> HarpoonControllers = new List<HarpoonController>();


    /**
     *  OnHookableObjectCollision handles collision between Stone and other GameObject
     *
     * @param stone: stone which detected collision
     * @param gameObject: GameObject which stone collided with
     */
    public static void OnHookableObjectCollision(Stone stone, GameObject gameObject)
    {
        if (gameObject.GetComponent<ProjectileCollision>())
        {
            AttachHookableObjectToProjectile(stone, gameObject);
        }
        else if (gameObject.CompareTag("Workshop") &&
                 IsStoneInInventory(stone,
                     gameObject.GetComponent<Workshop>().GetInventory().GetComponent<Inventory>()) &&
                 gameObject.GetComponent<Workshop>().IsEmpty())
        {
            SetOnDeselectParentOfStone(stone, gameObject.GetComponent<CanHoldHookableObject>());
        }
    }

    /**
     *  OnHookableObjectCollision handles collision between Item and other GameObject
     *
     * @param item: item which detected collision
     * @param gameObject: GameObject which item collided with (should be projectile)
     */
    public static void OnHookableObjectCollision(Item item, GameObject gameObject)
    {
        if (gameObject.GetComponent<ProjectileCollision>())
        {
            AttachHookableObjectToProjectile(item, gameObject);
        }
    }

    /**
     * SetOnDeselectParentOfStone sets the onDeselectParent of the stone
     *
     * @param stone: stone which is selected
     * @param canHoldHookableObject: the parent to set
     */
    public static void SetOnDeselectParentOfStone(Stone stone, CanHoldHookableObject canHoldHookableObject)
    {
        stone.SetOnDeselectParent(canHoldHookableObject);
    }

    /**
     * IsStoneInInventory checks if the stone is in the inventory
     *
     * @param stone: stone which is selected
     * @param inventory: inventory to search the stone in
     *
     * @return true if stone is in inventory, else false
     */
    public static bool IsStoneInInventory(Stone stone, Inventory inventory)
    {
        return inventory.StoneInInventory(stone);
    }


    /**
     * action needed when Stone is made draggable
     *
     * @param stone: Stone to be set draggable
     */
    public static void EnableStoneDraggable(Stone stone)
    {
        stone.SetDraggable(true);
        //TODO: probably some more
    }

    /**
     * Called after Collision of HookableObject and Projectile
     *
     * @param hookableObject: object which is attached to projectile
     * @param projectileGameObject: projectile where hookableObject is attached to
     */
    private static void AttachHookableObjectToProjectile(HookableObject hookableObject, GameObject projectileGameObject)
    {
        foreach (var harpoonController in HarpoonControllers)
        {
            // if there is a reason to hook stone, harpoon controller will return true
            if (harpoonController.NotifyCollisionWithHookableObject(hookableObject, projectileGameObject))
            {
                //TODO: dont use find every time
                GameObject.Find("StoneSpawner").GetComponent<StoneSpawner>().DeleteHookableObject(hookableObject);
                hookableObject.SetTransformParent(projectileGameObject.transform);
                hookableObject.SetLayerToDraggableLayer();
                //parent not needed right now, but available for future
                hookableObject.SetParent(projectileGameObject);
            }
        }
    }

    /**
     * AddHarpoonController adds HarpoonController to _harpoonControllers receiver list
     *
     * @param harpoonController HarpoonController added to list of observers
     */
    public static void AddHarpoonController(HarpoonController harpoonController)
    {
        HarpoonControllers.Add(harpoonController);
    }

    /**
     * RemoveHarpoonController removes HarpoonController from _harpoonControllers receiver list
     *
     * @param harpoonController HarpoonController to be removed from list of receivers
     */
    public static void RemoveHarpoonController(HarpoonController harpoonController)
    {
        HarpoonControllers.Remove(harpoonController);
    }

    /**
     * StoneToInventory handles adding and functionality if the inventory is full
     *
     * @param stone The stone to put into the inventory
     * @param inventory The inventory the stone is added to
     */
    public static void StoneToInventory(Stone stone, Inventory inventory)
    {
        var position = inventory.AddToInventory(stone);
        NotifyHarpoonControllersRemoveHookableObject(stone);
        if (position.HasValue)
        {
            stone.SetLayerToDraggableLayer();
            stone.SetParent(inventory.gameObject);
            stone.SetPosition(position.Value);
            stone.SetTransformParent(null);
            EnableStoneDraggable(stone);
        }
        else
        {
            stone.DestroyHookableObject();
        }
    }

    /**
     * StoneToWorkshop handles adding of stones to the workshop
     *
     * @param stone: the stone to add to the workshop
     * @param workshop: the workshop to add a stone to
     */
    public static void StoneToWorkshop(Stone stone, Workshop workshop)
    {
        stone.MakeScalableAndRotatable();
        stone.SetParent(workshop.gameObject);
        workshop.SetChild(stone);
    }

    /**
     * Handles behaviour for removing stones from workshop
     *
     * @param stone: stone to remove from the workshop
     */
    public static void StoneFromWorkshop(Stone stone)
    {
        stone.DisableScalableAndRotatable();
    }

    /**
     * OnDeselectOnCanHoldHookableObject is called when a stone is deselected over a canHoldHookableObject
     *
     * @param stone: stone which is selected
     * @param canHoldHookableObject: object under the stone
     */
    public static void OnDeselectOnCanHoldHookableObject(Stone stone, CanHoldHookableObject canHoldHookableObject)
    {
        if (canHoldHookableObject.GetComponent<Workshop>())
        {
            StoneToWorkshop(stone, canHoldHookableObject.GetComponent<Workshop>());
        }
    }


    /**
     * NotifyHarpoonControllersRemoveHookableObject removes the hookableObject from the harpoon controller
     *
     * @param hookableObject: object to remove
     */
    private static void NotifyHarpoonControllersRemoveHookableObject(HookableObject hookableObject)
    {
        foreach (var harpoonController in HarpoonControllers)
        {
            harpoonController.NotifyRemoveHookableObject(hookableObject);
        }
    }

    /**
     * handles OnWoundIn event, called by HarpoonController
     *
     * @param hookableObject object attached to projectile to be handled
     * @param inventory of player base where WoundIn event happened
     */
    public static void OnWoundIn(HookableObject hookableObject, Inventory inventory)
    {
        hookableObject.OnWoundIn(inventory);
    }

    /**
     * ActivateItem activates the item
     *
     * @param Item item
     */
    public static void ActivateItem(Item item)
    {
    }

    /**
     * remove a stone from canHoldHookableObject. Caution: no new parent for stone is set at this point! Instead this has to be done in event of new parent.
     *
     * @param stone to remove
     * @param canHoldHookableObject where stone should be removed
     */
    public static void RemoveStoneFromCanHoldHookableObject(Stone stone, CanHoldHookableObject canHoldHookableObject)
    {
        canHoldHookableObject.RemoveStone(stone);
    }

    /**
     * remove a stone from gameObject
     *
     * @param stone to remove
     * @param gameObject where stone should be removed
     */
    public static void RemoveStoneFromCanHoldHookableObject(Stone stone, GameObject gameObject)
    {
        RemoveStoneFromCanHoldHookableObject(stone, gameObject.GetComponent<CanHoldHookableObject>());
    }

    /**
     * returns designated Vector3 position of stone from any CanHoldHookableObject knowing this stone
     *
     * @param parent CanHoldHookableObject where stone is part of
     * @param stone to find its position to
     */
    public static Vector3 GetParentPositionOfChildStone(CanHoldHookableObject parent, Stone stone)
    {
        return parent.GetPositionOfStoneChild(stone);
    }
}