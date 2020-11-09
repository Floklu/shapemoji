using UnityEngine;

/**
 * @brief Work Area Class
 * This Class uses Collision Events
 * Following Functionality was implemented: Stone Centering and assigning Work Object
 * @author Andrei Dziubenka
 * @date 09.11.2020
 */
public class WorkArea : MonoBehaviour
{
    /**
     * Current Work Object
     */
    private GameObject _workObject;

    /// <summary>
    /// Trigger Event
    /// Assigns Stone to Work Area
    /// </summary>
    /// <param name="other">Collision Object</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Stone"))
        {
            _workObject = other.gameObject;
        }
    }

    /// <summary>
    /// Centers Stone at Work Area
    /// </summary>
    public void PutObject()
    {
        if (_workObject != null)
        {
            _workObject.transform.Translate(transform.position - _workObject.transform.position);
        }
    }
}