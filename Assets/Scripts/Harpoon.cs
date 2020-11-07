using UnityEngine;


public class Harpoon : MonoBehaviour
{
    [SerializeField] private float startRotationZ;

    private bool onHarpoon;

    private Vector3 initialPosition;

    private void OnMouseDown()
    {
        onHarpoon = true;
        initialPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        onHarpoon = false;
    }

    private void FixedUpdate()
    {
        if (onHarpoon && initialPosition != Camera.main.ScreenToWorldPoint(Input.mousePosition))
        {
            var difference = initialPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            difference.Normalize();

            var rotationZ = (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg) - 90f;

            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }
    }
}