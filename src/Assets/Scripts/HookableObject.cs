using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor;
using UnityEngine;


//TODO: at critical number of lines cut into multiple .cs files

/**
 * abstract class HookableObject is parent class of all GameObject which can be hooked by projectile
 *
 *  */
public abstract class HookableObject : MonoBehaviour
{
    private GameObject _parent;

    /**
     * on Start() set layer to PlayingFieldLayer
     */
    protected virtual void Start()
    {
        ChangeLayerPlayingFieldLayer();
    }

    /**
     * Calls for action at controller on collision with
     *
     * @param Collider other
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        HookableObjectController.OnHookableObjectCollision(this, other.gameObject);
    }

    /**
     * change collision layer to PlayingFieldLayer
     */
    public void ChangeLayerPlayingFieldLayer()
    {
        this.gameObject.layer = LayerMask.NameToLayer("PlayingFieldLayer");
    }

    /**
     * sets transform parent so movement is joined
     */
    public void SetTransformParent(Transform parentTransform)
    {
        this.transform.parent = parentTransform;
    }
    
    /**
     * OnWoundIn is called when the Harpoon is wound in
     * Source: branch 414
     * ToDo: Change GameObject to Inventory
     */
    public abstract void OnWoundIn(GameObject inventory);

    
}

/**
 * class Stone inherited from HookableObject
 *
 * OnStart set to layer Playing field
 */
public class Stone : HookableObject
{
    private bool _draggable;

    protected override void Start()
    {
        base.Start();
    }

    /**
     * change collision layer to DraggableLayer
     */
    public void ChangeLayerDraggableLayer()
    {
        this.gameObject.layer = LayerMask.NameToLayer("DraggableLayer");
    }
    
    /**
     * Source: Branch 414
     * gets called by controller when WoundIn event is triggered. calls StoneToInventory
     *
     * @param inventory where stone is put to
     */
    public override void OnWoundIn(GameObject inventory)
    {
        HookableObjectController.StoneToInventory(this, inventory);
    }


    /**
     * sets property _draggable
     *
     * @param bool state: bool _draggable is set to
     */
    public void SetDraggable(bool state)
    {
        _draggable = state;
    }
}

/**
 * class Item inherited from HookableObject
 *
 * future: will hold some 
 */
public class Item : HookableObject
{
    
    /**
     * Source: branch 414
     * ToDo: change GameObject to Inventory
     *  gets called by controller on wound in event
     *
     * @param inventory belonging to the player base
     */
    public override void OnWoundIn(GameObject inventory)
    {
        throw new System.NotImplementedException();
    }

}