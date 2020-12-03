using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

/**
 * Class Workshop contains the workshop functionality
 */
public class Workshop : CanHoldHookableObject
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Team;
    
    private Stone _child;


    /**
     * SetChild sets the child of the workshop
     *
     * @param stone: child to set
     */
    public void SetChild(Stone stone)
    {
        _child = stone;
    }

    /**
     * IsEmpty checks if the workshop is empty
     *
     * @return true if empty, else false
     */
    public bool IsEmpty()
    {
        return _child == null;
    }

    /**
     * GetPositionOfStoneChild gets the postition of the workshop
     *
     * @param stone: stone to check
     *
     * @return the position of the workshop
     */
    public override Vector3 GetPositionOfStoneChild(Stone stone)
    {
        return transform.position;
    }

    /**
     * RemoveStone removes the child from the workshop
     *
     * @param stone: stone to remove
     */
    public override void RemoveStone(Stone stone)
    {
        if (stone.Equals(_child))
        {
            _child = null;
            HookableObjectController.DisableScalableAndRotatable(stone);
        }

    }

    public override bool StoneToCanHoldHookableObject(Stone stone)
    {
        HookableObjectController.StoneToWorkshop(stone, this);
        return true;
    }

    public override bool IsStoneInCanHoldHookableObject(Stone stone)
    {
        //stone cannot be null, but child can, so needs to be called this way
        return stone.Equals(_child);
    }

    public override Player GetPlayer()
    {
        return Player.GetComponent<Player>();
    }

    public override Team GetTeam()
    {
        return Team.GetComponent<Team>();
    }
}
