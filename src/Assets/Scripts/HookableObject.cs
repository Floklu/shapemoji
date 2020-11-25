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
    protected GameObject _parent;
    

    /**
     * on Start() set layer to PlayingFieldLayer
     */
    protected virtual void Start()
    {
        SetLayerToPlayingFieldLayer();
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
    public void SetLayerToPlayingFieldLayer()
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
     * SetParent sets the parent of this gameobject
     *
     * @param GameObject parent: parent to set
     */
    public void SetParent(GameObject parent)
    {
        _parent = parent;
    }

    /**
     * set new parent and return old parent
     *
     * @returns parent before changing
     * @param newParent is the parent given to SetParent
     */
    public GameObject ChangeParent(GameObject newParent)
    {
        GameObject oldParent = _parent;
        SetParent(newParent);
        return oldParent;
    }

    /**
     * SetPosition sets the position of the hookable object
     *
     * @param position The position to set the hookable object to
     */
    public void SetPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }

    /**
     * OnWoundIn is called when the Harpoon is wound in
     */
    public abstract void OnWoundIn(Inventory inventory);
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
     * gets called by controller when WoundIn event is triggered. calls StoneToInventory
     *
     * @param inventory where stone is put to
     */
    public override void OnWoundIn(Inventory inventory)
    {
        HookableObjectController.StoneToInventory(this, inventory);
    }
    
    /**
     * change collision layer to DraggableLayer
     */
    public void SetLayerToDraggableLayer()
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
        /*
 // to move the stone with touch
 gameObject.AddComponent<LeanDragTranslate>();
 // to only move the stone you touch
 gameObject.AddComponent<LeanSelectable>();
 // to deselect when not touching the stone anymore
 gameObject.GetComponent<LeanSelectable>().DeselectOnUp = true;
 */
    }

    /**
     * gets called by lean event, when releasing stone from drag and puts it back to parent position
     */
    public void OnDeselectOnUp()
    {
        transform.position = HookableObjectController.GetParentPositionOfChildStone(_parent.GetComponent<CanHoldHookableObject>(), this);
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
     *  gets called by controller on wound in event
     *
     * @param inventory belonging to the player base
     */
    public override void OnWoundIn(Inventory inventory)
    {
        throw new System.NotImplementedException();
    }
}