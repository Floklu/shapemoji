using System;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using Random = UnityEngine.Random;

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
        try //catch, if no valid reference to camera exists 
        {
            if (camera is null) return false;
            var position = camera.ScreenToWorldPoint(finger.ScreenPosition);
            return collider.OverlapPoint(position);
        }
        catch (MissingReferenceException e)
        {
            return false;
        }
    }

    /**
     * Randomizes the position of the contents of a list
     *
     * @param list the list to shuffle
     */
    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            int k = Random.Range(0, n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}