using UnityEngine;

/**
 * performs a smooth transition for a gameObject
 */
public class SmoothTransition : MonoBehaviour
{
    public float RemainingTime { get; set; }
    public float InitialTime { get; set; }
    public Vector3 Direction { get; set; }
    public Vector3 TargetPosition { get; set; }

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
            transform.position = TargetPosition;
            //TODO trigger OnExit Event here
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
    public static void AddTransition(GameObject obj, Vector3 targetPosition, float transitionTime)
    {
        var transistion = obj.AddComponent<SmoothTransition>();
        transistion.Direction = targetPosition - obj.transform.position;
        transistion.TargetPosition = targetPosition;
        transistion.RemainingTime = transitionTime;
        transistion.InitialTime = transitionTime;
    }
}