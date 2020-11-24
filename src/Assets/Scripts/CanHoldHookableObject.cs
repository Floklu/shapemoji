using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CanHoldHookableObject : MonoBehaviour
{
    public abstract Vector3 GetPositionOfStoneChild(Stone stone);
}
