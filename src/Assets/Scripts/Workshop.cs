using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class Workshop contains the workshop functionality
 */
public class Workshop : CanHoldHookableObject
{
    [SerializeField] private GameObject myBase;

    public GameObject GetBase()
    {
        return myBase;
    }

    public override Vector3 GetPositionOfStoneChild(Stone stone)
    {
        return transform.position;
    }

    public override void RemoveStone(Stone stone)
    {
        throw new System.NotImplementedException();
    }
}