using Lean.Touch;
using UnityEngine;

/**
 * class Stone inherited from HookableObject
 *
 * OnStart set to layer Playing field
 */
public class Stone : HookableObject
{
    private bool _draggable;

    // Parent that changes when the stone collides with a CanHoldHookableObject
    private CanHoldHookableObject _onDeselectParent;


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
     * OnTriggerExit2D is called when the Collider other has stopped touching the trigger.
     *
     * @param Collider2D other
     */
    private void OnTriggerExit2D(Collider2D other)
    {
        _onDeselectParent = null;
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
     * SetOnDeselectParent sets the parent of the current frame
     *
     * @param canHoldHookableObject the parent to set
     */
    public void SetOnDeselectParent(CanHoldHookableObject canHoldHookableObject)
    {
        _onDeselectParent = canHoldHookableObject;
    }

    /**
     * sets property _draggable
     *
     * @param bool state: bool _draggable is set to
     */
    public void SetDraggable(bool state)
    {
        _draggable = state;
        if (state)
            MakeDraggable();
        else
            MakeUnDraggable();
    }

    /**
     * make Stone unselectable and therefore undraggable
     */
    private void MakeUnDraggable()
    {
        var leanSelectable = gameObject.GetComponent<LeanSelectable>();
        leanSelectable.enabled = false;
    }

    /**
     * gets called by lean event, when releasing stone from drag and puts it back to parent position or first sets the new parent
     */
    public void OnDeselectOnUp()
    {
        if (_onDeselectParent != null && _onDeselectParent != Parent.GetComponent<CanHoldHookableObject>())
        {
            HookableObjectController.RemoveStoneFromCanHoldHookableObject(this, Parent);
            HookableObjectController.OnDeselectOnCanHoldHookableObject(this, _onDeselectParent);
        }

        transform.position =
            HookableObjectController.GetParentPositionOfChildStone(Parent.GetComponent<CanHoldHookableObject>(), this);
    }

    /**
     * MakeDraggable adds the Lean Touch Scripts and functionality to the stone to make it draggable
     */
    private void MakeDraggable()
    {
        //set selectable
        gameObject.AddComponent<LeanSelectable>();
        //move on drag
        gameObject.AddComponent<LeanDragTranslate>();
        //set deselectable
        var leanSelectable = gameObject.GetComponent<LeanSelectable>();
        leanSelectable.DeselectOnUp = true;
        leanSelectable.IsolateSelectingFingers = true;
        leanSelectable.OnDeselect.AddListener(OnDeselectOnUp);
    }

    /**
     * adds or enables Lean Touch Scripts for Rotating and Scaling Stone with two fingers
     */
    public void MakeScalableAndRotatable()
    {
        var scalable = gameObject.GetComponent<LeanPinchBoundedScale>();
        var rotatable = gameObject.GetComponent<LeanTwistRotate>();
        if (scalable == null) scalable = gameObject.AddComponent<LeanPinchBoundedScale>();
        if (rotatable == null) rotatable = gameObject.AddComponent<LeanTwistRotate>();
        scalable.enabled = true;
        rotatable.enabled = true;
    }

    /**
     * Disables Lean Touch Scripts for Rotating and Scaling Stone with two fingers
     */
    public void DisableScalableAndRotatable()
    {
        var scalable = gameObject.GetComponent<LeanPinchBoundedScale>();
        var rotatable = gameObject.GetComponent<LeanTwistRotate>();
        if (scalable != null) scalable.enabled = false;
        if (rotatable != null) rotatable.enabled = false;
    }

    /**
     * renabables leanSelectable, only to be called if Stone has Component LeanSelectable
     */
    public void ReEnableStoneDraggable()
    {
        var leanSelectable = gameObject.GetComponent<LeanSelectable>();
        leanSelectable.enabled = true;
    }
}