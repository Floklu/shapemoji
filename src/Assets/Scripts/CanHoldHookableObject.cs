using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * abstract parent class for Inventory, Workshop and Emoji
 */
public abstract class CanHoldHookableObject : MonoBehaviour
{
    /**
     * get the position where a specific stone should be placed
     *
     * @param stone to find its position to
     */
    public abstract Vector3 GetPositionOfStoneChild(Stone stone);
}
