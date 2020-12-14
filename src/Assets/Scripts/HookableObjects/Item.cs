using System;
using UnityEngine;

/**
 * class Item inherited from HookableObject
 * 
 * future: will hold some
 */
public class Item : HookableObject
{
    /**
     * Calls for action at controller on collision with
     *
     * @param Collider other
     */
    public override void OnTriggerEnter2D(Collider2D other)
    {
        HookableObjectController.OnHookableObjectCollision(this, other.gameObject);
    }

    /**
     * gets called by controller on wound in event
     * 
     * @param inventory belonging to the player base
     */
    public override void OnWoundIn(Inventory inventory)
    {
        HookableObjectController.ActivateItem(this);
    }

    /**
     * Handles behaviour, after item has been activated
     */
    public virtual void OnActivate()
    {
        throw new NotImplementedException();
    }
}