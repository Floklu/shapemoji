using UnityEngine;

/**
 * Class Workshop contains the workshop functionality
 */
public class Workshop : CanHoldHookableObject
{
    [SerializeField] private GameObject inventory;
    private Stone _child;

    public GameObject GetInventory()
    {
        return inventory;
    }

    public void SetChild(Stone stone)
    {
        _child = stone;
    }

    public bool IsEmpty()
    {
        return _child == null;
    }

    public override Vector3 GetPositionOfStoneChild(Stone stone)
    {
        return transform.position;
    }

    public override void RemoveStone(Stone stone)
    {
        _child = null;
    }
}