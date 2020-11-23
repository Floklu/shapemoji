using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor;
using UnityEngine;
using Lean.Touch;


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
    protected virtual void OnWoundIn()
    {
    }
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
    public void SetLayerToDraggableLayer()
    {
        this.gameObject.layer = LayerMask.NameToLayer("DraggableLayer");
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

    /**
     * OnWoundIn changes the stone so it can be dragged and not get hit by projectiles 
     */
    protected override void OnWoundIn()
    {
        SetLayerToDraggableLayer();
        // still WIP
        gameObject.AddComponent<LeanDragTranslate>();
    }
}

/**
 * class Item inherited from HookableObject
 *
 * future: will hold some 
 */
public class Item : HookableObject
{
    protected override void OnWoundIn()
    {
        base.OnWoundIn();
    }
}