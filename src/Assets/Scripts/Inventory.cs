using System;
using UnityEngine;

/**
 * Class Inventory contains the inventory functionality
 */
public class Inventory : CanHoldHookableObject
{
    [SerializeField] public GameObject[] slots = new GameObject[4];
    private readonly bool[] _slotIsFull = new bool[4];
    private readonly Stone[] _stoneInSlot = new Stone[4];


    private void Start()
    {
        _player = gameObject.GetComponentInParent<Player>();
        _team = gameObject.GetComponentInParent<Team>();
    }

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
            if (_slotIsFull[i] == false)
            {
                _slotIsFull[i] = true;
                _stoneInSlot[i] = stone;
                return slots[i].transform.position;
            }

        return null;
    }

    /**
     * remove a given stone from inventory, making its slot again
     *
     * @param stone to remove
     */
    public override void RemoveStone(Stone stone)
    {
        var slotIndex = GetIndexOfStoneInSlot(stone);
        if (slotIndex == -1) return;
        _slotIsFull[slotIndex] = false;
        _stoneInSlot[slotIndex] = null;
    }

    /**
     * not yet implemented
     * 
     * TODO: Future work add AddStoneToInventory should start by refactoring to use this method instead
     */
    public override bool StoneToCanHoldHookableObject(Stone stone)
    {
        throw new NotImplementedException();
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
        if (slotIndex == -1) return Vector3.zero;
        return slots[slotIndex].transform.position;
    }

    /**
     * returns index of slot containing Stone stone,returns -1 if not found
     *
     * @param stone to find its position
     */
    private int GetIndexOfStoneInSlot(Stone stone)
    {
        for (var i = 0; i < _stoneInSlot.Length; i++)
            if (_stoneInSlot[i] != null && _stoneInSlot[i].Equals(stone))
                return i;

        return -1;
    }

    /**
     * StoneInInventory checks if stone is in this inventory
     *
     * @param stone: stone to check
     */
    public override bool IsStoneInCanHoldHookableObject(Stone stone)
    {
        return GetIndexOfStoneInSlot(stone) != -1;
    }
}