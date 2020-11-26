using UnityEngine;

/**
 * Class Inventory contains the inventory functionality
 */
public class Inventory : CanHoldHookableObject
{
    private readonly bool[] _slotIsFull = new bool[4];
    private readonly Stone[] _stoneInSlot = new Stone[4];
    [SerializeField] public GameObject[] slots = new GameObject[4];

    /**
     * AddToInventory adds a Stone to an empty slot in the inventory
     *
     * @param stoneGameObject the game object to add
     *
     * @return true if stone could be added, false otherwise 
     */
    public Vector3? AddToInventory(Stone stone)
    {
        for (var i = 0; i < slots.Length; i++)
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

    /**
     * remove a given stone from inventory, making its slot again
     *
     * @param stone to remove
     */
    public void RemoveFromInventory(Stone stone)
    {
        var slotIndex = GetIndexOfStoneInSlot(stone);
        if (!(slotIndex >= 0 & slotIndex < 4)) return;
        _slotIsFull[slotIndex] = false;
        _stoneInSlot[slotIndex] = null;
    }

    /**
     * returns the position of stone inventory slot belonging to stone
     *
     * @param stone belonging to slot
     */
    //TODO: handle index out of boundary exception?
    public override Vector3 GetPositionOfStoneChild(Stone stone)
    {
        var slotIndex = GetIndexOfStoneInSlot(stone);
        if (!(slotIndex >= 0 & slotIndex < 4)) return Vector3.zero;
        return slots[slotIndex].transform.position;
    }

    private int GetIndexOfStoneInSlot(Stone stone)
    {
        var slotIndex = -1;
        for (var i = 0; i < _stoneInSlot.Length; i++)
        {
            if (_stoneInSlot[i].Equals(stone))
            {
                slotIndex = i;
            }
        }

        return slotIndex;
    }
}