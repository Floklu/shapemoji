using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class LeanHandler 
{
    /**
     * MakeDraggable adds the Lean Touch Scripts and functionality to the gameObject to make it draggable
     */
    public static void MakeDraggable(GameObject gameObject)
    {
        //set selectable
        gameObject.AddComponent<LeanSelectable>();
        //move on drag
        gameObject.AddComponent<LeanDragTranslate>();
        //set deselectable
        var leanSelectable = gameObject.GetComponent<LeanSelectable>();
        leanSelectable.DeselectOnUp = true;
        leanSelectable.IsolateSelectingFingers = true;
    }
}
