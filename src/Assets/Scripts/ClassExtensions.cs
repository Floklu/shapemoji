using Lean.Touch;
using UnityEngine;

/**
 * Adds new Functions to existing Classes
 */
public static class ClassExtensions
{
    /**
     * Extends LeanFinger with check, if Finger overlaps with given collider
     *
     * @param finger LeanFinger from LeanTouch library
     * @param collider the bounds of the object
     * @param camera used to translate the camera axis to world axis
     */
    public static bool CollidesWithGameObject(this LeanFinger finger, Collider2D collider, Camera camera)
    {
        var position = camera.ScreenToWorldPoint(finger.ScreenPosition);
        return collider.OverlapPoint(position);
    }
}