using UnityEngine;

/**
 * Class Workshop contains the workshop functionality
 */
public class Workshop : CanHoldHookableObject
{
    [SerializeField] private GameObject myBase;
    private Stone _child;

    public GameObject GetBase()
    {
        return myBase;
    }

    public void SetChild(Stone stone)
    {
        _child = stone;
    }

    public Stone GetChild()
    {
        return _child;
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