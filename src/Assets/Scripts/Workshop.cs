using UnityEngine;

/**
 * Class Workshop contains the workshop functionality
 */
public class Workshop : CanHoldHookableObject
{
    [SerializeField] private GameObject inventory;
    private Stone _child;

    /**
     * GetInventory returns the inventory of the player from were the workshop belongs to
     *
     * @return the inventory
     */
    public GameObject GetInventory()
    {
        return inventory;
    }

    /**
     * SetChild sets the child of the workshop
     *
     * @param stone: child to set
     */
    public void SetChild(Stone stone)
    {
        _child = stone;
    }

    /**
     * IsEmpty checks if the workshop is empty
     *
     * @return true if empty, else false
     */
    public bool IsEmpty()
    {
        return _child == null;
    }

    /**
     * GetPositionOfStoneChild gets the postition of the workshop
     *
     * @param stone: stone to check
     *
     * @return the position of the workshop
     */
    public override Vector3 GetPositionOfStoneChild(Stone stone)
    {
        return transform.position;
    }

    /**
     * RemoveStone removes the child from the workshop
     *
     * @param stone: stone to remove
     */
    public override void RemoveStone(Stone stone)
    {
        _child = null;
    }
}