using UnityEngine;
using UnityEngine.Serialization;

/**
 * Class Workshop contains the workshop functionality
 */
public class Workshop : CanHoldHookableObject
{
    [FormerlySerializedAs("Player")] [SerializeField] private GameObject player;
    [FormerlySerializedAs("Team")] [SerializeField] private GameObject team;

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

    /**
     * implementation of parent class, adding Stone to Workshop
     *
     * @param stone Stone To Add
     */
    public override bool StoneToCanHoldHookableObject(Stone stone)
    {
        HookableObjectController.StoneToWorkshop(stone, this);
        return true;
    }

    /**
     * stone equals _child?
     *
     * @param Stone to compare
     */
    public override bool IsStoneInCanHoldHookableObject(Stone stone)
    {
        //stone cannot be null, but child can, so needs to be called this way
        return stone.Equals(_child);
    }

    /**
     * get player owning Workshop
     *
     */
    public override Player GetPlayer()
    {
        return player.GetComponent<Player>();
    }

    /**
     * get team of the player owning Workshop
     */
    public override Team GetTeam()
    {
        return team.GetComponent<Team>();
    }
}
