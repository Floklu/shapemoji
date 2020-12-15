using Lean.Touch;
using UnityEngine;
using UnityEngine.Events;

namespace Transition
{
    /**
 * performs a smooth transition for a gameObject
 */
    public class SmoothTransition : MonoBehaviour
    {
        public float RemainingTime { get; set; }
        public float InitialTime { get; set; }
        public Vector3 Direction { get; set; }
        public Vector3 TargetPosition { get; set; }

        public LeanSelectable Selectable { get; set;}
    
        [Tooltip("Triggered when the transition ends")]
        public UnityEvent onExit = new UnityEvent();

        //calculate new Postion for the gameObject
        private void Update()
        {
            if (Time.deltaTime < RemainingTime)
            {
                transform.position += Direction * (Time.deltaTime * (1 / InitialTime));
                RemainingTime -= Time.deltaTime;
            }
            else
            {
                if (Selectable != null)
                {
                    Selectable.enabled = true;
                }
                transform.position = TargetPosition;
                onExit.Invoke();
                Destroy(this);
            }
        }


        /**
     * adds SmoothTransition behaviour to given GameObject, transfers over specified time period to the target position
     * 
     * @param obj GameObject, which should be moved
     * @param targetPosition where the gameObject should be moved to
     * @param transistionTime timePeriod over which the transition is done
     */
        public static SmoothTransition AddTransition(GameObject obj, Vector3 targetPosition, float transitionTime)
        {
            var transition = obj.AddComponent<SmoothTransition>();
            transition.Direction = targetPosition - obj.transform.position;
            transition.TargetPosition = targetPosition;
            transition.RemainingTime = transitionTime;
            transition.InitialTime = transitionTime;

            transition.Selectable = obj.GetComponent<LeanSelectable>();
            if (transition.Selectable != null)
            {
                transition.Selectable.enabled = false;
            }
        
            return transition;
        }
    }
}