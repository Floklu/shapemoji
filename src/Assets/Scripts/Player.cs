using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Workshop _workshop;
    private Inventory _inventory;
    private List<Stone> _stones;

    
    // Start is called before the first frame update
    void Start()
    {
        _inventory = gameObject.GetComponent<Inventory>();
        _workshop = gameObject.GetComponent<Workshop>();
        _stones = new List<Stone>();
    }

    public Inventory GetInventory()
    {
        return _inventory;
    }

    public Workshop GetWorkshop()
    {
        return _workshop;
    }
    public void AddStone(Stone stone)
    {
        _stones.Add(stone);
    }

    public void RemoveStone(Stone stone)
    {
        _stones.Remove(stone);
    }
    
    public bool ContainsStone(Stone stone)
    {
        //will cause null pointer exception if contains called and some stones are null if deleted?
        return _stones.Contains(stone);
    }

}
