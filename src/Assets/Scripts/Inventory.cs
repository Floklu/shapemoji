using System.Collections.Generic;
using UnityEngine;

/**
 * Class Inventory contains the inventory functionality
 */
public class Inventory : CanHoldHookableObject
{
    private readonly bool[] _slotIsFull = new bool[4];
    private readonly List<Stone> _stoneInSlot = new List<Stone>(4);



    [SerializeField] private GameObject[] slots = new GameObject[4];

    /**
     * AddToInventory adds a Stone to an empty slot in the inventory
     *
     * @param stoneGameObject the game object to add
     *
     * @return true if stone could be added, false otherwise 
     */
    public Vector3? AddToInventory(Stone stone)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (_slotIsFull[i] == false)
            {
                _slotIsFull[i] = true;
                _stoneInSlot[i] = stone;
                return slots[i].transform.position;
            }
        }

        return null;
    }

    
    public void RemoveFromInventory(Stone stone)
    {
        var i = _stoneInSlot.IndexOf(stone);
        _slotIsFull[i] = false;
        _stoneInSlot.RemoveAt(i);
    }
    

    public override Vector3 GetPositionOfStoneChild(Stone stone)
    {
        var i = _stoneInSlot.IndexOf(stone);
        return slots[i].transform.position;
    }
}