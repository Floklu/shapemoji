using UnityEngine;

public class WorkArea : MonoBehaviour
{
    private Collider2D _workObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Stone"))
        {
            _workObject = other;
        }
    }

    public void PutObject()
    {
        if (_workObject != null)
        {
            _workObject.transform.Translate(transform.position - _workObject.transform.position);
        }
    }
}