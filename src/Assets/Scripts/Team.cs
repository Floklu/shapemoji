using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void AddStone(Stone stone)
    {
        _stones.Add(stone);
    }

    public bool ContainsStone(Stone stone)
    {
        //will cause null pointer exception if contains called and some stones are null if deleted?
        return _stones.Contains(stone);
    }

    public ScoreArea GetScoreArea()
    {
        return ScoreArea;
    }
}
