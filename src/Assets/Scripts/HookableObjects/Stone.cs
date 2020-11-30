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
     * Calls for action at controller on collision with
     *
     * @param Collider other
     */
    public override void OnTriggerEnter2D(Collider2D other)
    {
        HookableObjectController.OnHookableObjectCollision(this, other.gameObject);
    }

    /**
     * sets property _draggable
     *
     * @param bool state: bool _draggable is set to
     */
    public void SetDraggable(bool state)
    {
        _draggable = state;
        //set selectable
        gameObject.AddComponent<LeanSelectable>();
        //move on drag
        gameObject.AddComponent<LeanDragTranslate>();
        //set deselectable
        gameObject.GetComponent<LeanSelectable>().DeselectOnUp = true;
    }

    /**
     * gets called by lean event, when releasing stone from drag and puts it back to parent position
     */
    public void OnDeselectOnUp()
    {
        transform.position =
            HookableObjectController.GetParentPositionOfChildStone(Parent.GetComponent<CanHoldHookableObject>(), this);
    }
}