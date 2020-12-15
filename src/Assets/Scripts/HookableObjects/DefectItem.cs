using System.Linq;
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
            var randomPlayer = Random.Range(0, team.Players.Count);
            _player = team.Players[randomPlayer];
        }
        
        base.OnWoundIn(inventory);
    }

    public override void OnActivate()
    {
        if (_player != null) _player.IgniteHarpoon();
    }
}