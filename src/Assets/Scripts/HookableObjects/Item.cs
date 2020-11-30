using UnityEngine;

/**
 * class Item inherited from HookableObject
 *
 * future: will hold some 
 */
public class Item : HookableObject
{
    /**
     *  gets called by controller on wound in event
     *
     * @param inventory belonging to the player base
     */
    public override void OnWoundIn(Inventory inventory)
    {
        throw new System.NotImplementedException();
    }

    /**
     * Calls for action at controller on collision with
     *
     * @param Collider other
     */
    public override void OnTriggerEnter2D(Collider2D other)
    {
        HookableObjectController.OnHookableObjectCollision(this, other.gameObject);
    }
}