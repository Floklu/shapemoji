using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** class to store player wide information
 * 
 */
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

}
