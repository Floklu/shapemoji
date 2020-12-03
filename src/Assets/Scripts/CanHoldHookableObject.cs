using UnityEngine;

/**
 * abstract parent class for Inventory, Workshop and Emoji
 */
public abstract class CanHoldHookableObject : MonoBehaviour
{
    protected Player _player;
    protected Team _team;

    
    /**
     * get the position where a specific stone should be placed
     *
     * @param stone to find its position to
     */
    public abstract Vector3 GetPositionOfStoneChild(Stone stone);

    public abstract void RemoveStone(Stone stone);

    public abstract bool StoneToCanHoldHookableObject(Stone stone);

    public abstract bool IsStoneInCanHoldHookableObject(Stone stone);

    public virtual Player GetPlayer()
    {
        return _player;
    }

    public virtual Team GetTeam()
    {
        return _team;
    }
}