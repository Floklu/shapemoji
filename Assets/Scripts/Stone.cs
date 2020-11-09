using UnityEngine;

public class Stone : MonoBehaviour
{
    private Collider2D _currentWorkShop;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WorkArea"))
        {
            _currentWorkShop = other;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("WorkArea") && other==_currentWorkShop)
        {
            _currentWorkShop = null;
        }
    }


    /// <summary>
    /// Drag Method
    /// </summary>
    private void OnMouseDrag()
    {
        if (Camera.main != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }

    private void OnMouseUp()
    {
        if (_currentWorkShop != null)
        {
            _currentWorkShop.gameObject.GetComponent<WorkArea>().PutObject();
        }
    }
}