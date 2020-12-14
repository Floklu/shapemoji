using System;
using UnityEngine;

/**
 * class DefectItem inherited from Item
 * 
 * future: will hold some
 */
public class DefectItem : Item
{

    /**
     * gets called by controller on wound in event
     * 
     * @param inventory belonging to the player base
     */
    public override void OnWoundIn(Inventory inventory)
    {
        throw new NotImplementedException(); //TODO trigger defect specific behaviour
    }
}