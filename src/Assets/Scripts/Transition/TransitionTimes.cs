using UnityEngine;

namespace Transition
{
    /**
     * Contains information about the transition times
     */
    [CreateAssetMenu(fileName = "TransitionTimes", menuName = "ScriptableObjects/TransitionTimes", order = 0)]
    public class TransitionTimes : ScriptableObject
    {
        public float moveToInventory;
        public float snapBack;
        public float itemTransition;
    }
}