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

    /**
     * remove Stone from CanHookableObject, behaviour depends on type of CanHoldHookableObject
     *
     * @param stone Stone to add
     */
    public abstract void RemoveStone(Stone stone);


    /**
     * add Stone to CanHoldHookableObject, behaviour depends on Type
     *
     * @param stone Stone to add
     */
    public abstract bool StoneToCanHoldHookableObject(Stone stone);

    /**
     * check if stone is child of CanHoldHookableObject
     *
     * @param stone Stone to check
     */
    public abstract bool IsStoneInCanHoldHookableObject(Stone stone);

    /**
     * get player who owns this CanHoldHookableObject
     */
    public virtual Player GetPlayer()
    {
        return _player;
    }

    /**
     * get Team which this CanHoldHookableObject belongs to
     */
    public virtual Team GetTeam()
    {
        return _team;
    }
}