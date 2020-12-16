using System.Collections.Generic;
using UnityEngine;

/** class to store player wide information
 * 
 */
public class Player : MonoBehaviour
{
    public GameObject inventory;
    public GameObject workshop;
    public GameObject itemDefect;
    public GameObject harpoon;
    
    private Inventory _inventory;
    private List<Stone> _stones;
    private Workshop _workshop;


    // Start is called before the first frame update
    private void Start()
    {
        _inventory = inventory.GetComponent<Inventory>();
        _workshop = workshop.GetComponent<Workshop>();
        _stones = new List<Stone>();
    }

    /**
     * get Inventory belonging to Player
     */
    public Inventory GetInventory()
    {
        return _inventory;
    }

    /**
     * get workshop belonging to Player
     */
    public Workshop GetWorkshop()
    {
        return _workshop;
    }

    /**
     * add Stone to make them owned by Player
     *
     * @param Stone to make Player owner of
     */
    public void AddStone(Stone stone)
    {
        _stones.Add(stone);
    }

    /**
     * stone not longer owend by Player
     *
     * @param stone to remove
     */
    public void RemoveStone(Stone stone)
    {
        _stones.Remove(stone);
    }

    /**
     * check if Stone is owned by Player
     *
     * @param stone  Stone to check
     */
    public bool ContainsStone(Stone stone)
    {
        //will cause null pointer exception if contains called and some stones are null if deleted?
        return _stones.Contains(stone);
    }

    /**
     * Ignites the harpoon of the player and temporarily disables the functionality
     */
    public void IgniteHarpoon()
    {
        if (itemDefect != null) itemDefect.SetActive(true);
    }
}