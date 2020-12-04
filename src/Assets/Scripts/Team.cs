using System.Collections.Generic;
using UnityEngine;

/**
 * class Team to hold information that are team wide
 */
public class Team : MonoBehaviour
{
    [SerializeField] private ScoreArea ScoreArea;
 
    //private Player[] _players;
    private List<Stone> _stones;
    void Start()
    {
        //_players = gameObject.GetComponents<Player>();
        _stones = new List<Stone>();
    }

    /**
     * add Stone to team, Stone can still be added to CanHoldHookableObject
     *
     * @param stone to add
     */
    public void AddStone(Stone stone)
    {
        _stones.Add(stone);
    }

    /**
     * check if stone belongs to Team
     *
     * @param stone to check
     */
    public bool ContainsStone(Stone stone)
    {
        //will cause null pointer exception if contains called and some stones are null if deleted?
        return _stones.Contains(stone);
    }

    /**
     * get ScoreArea belonging to Team
     */
    public ScoreArea GetScoreArea()
    {
        return ScoreArea;
    }
}
