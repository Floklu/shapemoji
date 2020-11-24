using UnityEngine;

/**
 * Class Inventory contains the inventory functionality
 */
public class Inventory : MonoBehaviour
{
    private readonly bool[] _slotIsFull = new bool[4];

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
                return slots[i].transform.position;
            }
        }

        return null;
    }
}