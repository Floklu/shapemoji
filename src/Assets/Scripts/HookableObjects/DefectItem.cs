using System.Linq;
using Transition;
using UnityEngine;

/**
 * class DefectItem inherited from Item
 * 
 * activates defect on random harpoon on other team
 */
public class DefectItem : Item
{
    private Player _player;
    
    /**
     * gets called by controller on wound in event
     * 
     * @param inventory belonging to the player base
     */
    public override void OnWoundIn(Inventory inventory)
    {
        //get first instance of list of teams, which doesn't contain the given inventory
        var team = Game.Instance.Teams.First(x => !x.Players.Any(y => y.GetInventory().Equals(inventory)));
        
        if (team != null)
        {
            var players = team.Players.Where(x => x.itemDefect.activeSelf == false).ToList();
            if (players.Count > 0)
            {
                var randomPlayer = Random.Range(0, players.Count);
                _player = players[randomPlayer];    
            }
            
        }
        
        base.OnWoundIn(inventory);
    }

    public override void OnActivate()
    {
        if (_player != null)
        {
            var transition = SmoothTransition.AddTransition(gameObject, _player.harpoon.transform.position, Game.Instance.transitionTimes.itemTransition);
            transition.onExit.AddListener(() =>
            {
                _player.IgniteHarpoon();
                DestroyHookableObject();
            });
        }
        else
        {
            DestroyHookableObject();
        }
    }
}