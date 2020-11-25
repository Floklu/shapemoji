using System.Collections.Generic;
using Harpoon;
using UnityEngine;

/**
 * HookableObjectController is a static class and contains the functionality of Hookable Objects 
 */
public static class HookableObjectController
{
    //list of receivers
    private static readonly List<HarpoonController> _harpoonControllers = new List<HarpoonController>();


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
        foreach (var harpoonController in _harpoonControllers)
        {
            harpoonController.NotifyCollisionWithHookableObject(hookableObject, projectileGameObject);
        }
        

        hookableObject.SetTransformParent(projectileGameObject.transform);
        // need to change layer here so we can pass other stone during wind in
        hookableObject.SetLayerToDraggableLayer();
    }

    /**
     * AddHarpoonController adds HarpoonController to _harpoonControllers receiver list
     */
    public static void AddHarpoonController(HarpoonController harpoonController)
    {
        _harpoonControllers.Add(harpoonController);
    }

    /**
     * RemoveHarpoonController removes HarpoonController from _harpoonControllers receiver list
     */
    public static void RemoveHarpoonController(HarpoonController harpoonController)
    {
        _harpoonControllers.Remove(harpoonController);
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
        if (position.HasValue)
        {
            stone.SetLayerToDraggableLayer();
            stone.SetDraggable(true);
            stone.SetParent(inventory.gameObject);
            stone.SetPosition(position.Value);
        }
        else
        {
            // Task #417
        }
    }

    /**
     * handles OnWoundIn event, called by HarpoonContoler
     *
     * @param hookableObject object attached to projectile to be handeled
     * @param inventory of player base where WoundIn event happened
     */
    public static void OnWoundIn(HookableObject hookableObject, Inventory inventory)
    {
        hookableObject.OnWoundIn(inventory);
        Debug.Log("hit");
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
     * remove a stone from inventory. Caution: no new parent for stone is set at this point! Instead this has to be done in event of new parent.
     *
     * @param stone to remove
     * @param inventory where stone should be removed
     */
    public static void RemoveStoneFromInventory(Stone stone, Inventory inventory)
    {
        inventory.RemoveFromInventory(stone);
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