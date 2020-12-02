using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreArea : CanHoldHookableObject
{
    private List<Stone> _stones;
    // Start is called before the first frame update
    void Start()
    {
        _stones = new List<Stone>();
    }

    public void AddStone(Stone stone)
    {
        _stones.Add(stone);
        //lock n-1th stone
        HookableObjectController.DisableStoneDraggable(_stones[_stones.Count-2]);
    }

    public override Vector3 GetPositionOfStoneChild(Stone stone)
    {
        return HookableObjectController.GetPositionOfHookableObject(stone);
    }

    public override void RemoveStone(Stone stone)
    {
        _stones.Remove(stone);
        // reenable draggable on last stone
        HookableObjectController.EnableStoneDraggable(_stones[_stones.Count-1]);
    }
}
