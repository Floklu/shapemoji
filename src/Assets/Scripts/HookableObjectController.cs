using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Harpoon;
using Unity.Profiling;
using UnityEditor.PackageManager;
using UnityEditor.SceneManagement;
using UnityEngine;

/**
 * HookableObjectController is a static class and contains the functionality of Hookable Objects 
 */
public static class HookableObjectController
{
    //list of receivers
    private static readonly List<HarpoonController> _harpoonControllers = new List<HarpoonController>();

    /**Manages Collision Event of HookableObject (called by HookableObject Instance)
     *
     * @param hookableObject HookableObject which detected collision
     * @param otherObject GameObject which collided with HookableObject
     */
    public static void OnHookableObjectCollision(HookableObject hookableObject, GameObject gameObject)
    {
    }

    /**
     *  OnHookableObjectCollision handles collision between Stone and other GameObject
     *
     * @param stone: stone which detected collision
     * @param gameObject: GameObject which stone collided with
     */
    public static void OnHookableObjectCollision(Stone stone, GameObject gameObject)
    {
        if (gameObject.GetType() == typeof(ProjectileCollision))
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
        if (gameObject.GetType() == typeof(ProjectileCollision))
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
        //TODO: enable when implemented in harpoonController
        /*
        foreach (var harpoonController in harpoonControllers)
        {
            harpoonController.NotifyCollisionWithHookableObject(hookableObject, projectileGameObject)
        }
        */

        hookableObject.SetTransformParent(projectileGameObject.transform);
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
    public static void StoneToInventory(Stone stone, GameObject inventory)
    {
        var position = inventory.GetComponent<Inventory>().AddToInventory(stone);
        if (position.HasValue)
        {
            stone.OnWoundIn();
            stone.SetParent(inventory);
            stone.SetPosition(position.Value);
        }
        else
        {
            // Task #417
        }
    }

    /**
     * ActivateItem activates the item
     *
     * @param Item item
     */
    public static void ActivateItem(Item item)
    {
    }
}