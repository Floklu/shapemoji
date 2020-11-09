using UnityEngine;

/**
 * @brief Stone Class
 * This Class uses Event Handlers (Mouse and Collision Events).
 * Following Functionality was implemented: Stone Drag & Drop (Playing Field and Workshop) 
 * @author Andrei Dziubenka
 * @date 09.11.2020
 */
public class Stone : MonoBehaviour
{
    /**
     * Current Work Area
     */
    private GameObject _currentWorkArea;

    /// <summary>
    /// Trigger Event
    /// Is used to assign current Work Area
    /// </summary>
    /// <param name="other">Collision Object</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WorkArea"))
        {
            _currentWorkArea = other.gameObject;
        }
    }

    /// <summary>
    /// Trigger Event Handler
    /// Is used when Stone is dragged out from the WorkArea
    /// </summary>
    /// <param name="other">Collision Object</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("WorkArea") && other.gameObject == _currentWorkArea)
        {
            _currentWorkArea = null;
        }
    }

    /// <summary>
    /// Drag Method
    /// Is used to drag Stones
    /// </summary>
    private void OnMouseDrag()
    {
        if (Camera.main != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }

    /// <summary>
    /// Mouse Up Event Handler
    /// Is used to center the Stone at the Work Area
    /// </summary>
    private void OnMouseUp()
    {
        if (_currentWorkArea != null)
        {
            _currentWorkArea.GetComponent<WorkArea>().PutObject();
        }
    }
}