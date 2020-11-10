using UnityEngine;

/**
 * Class Harpoon contains functionality for the rotation and shooting of the harpoon.
 */
public class Harpoon : MonoBehaviour
{
    private bool onDrag;

    private Vector3 initialPosition;

    private Camera mainCamera;

    private Collider2D harpoonCollider;


    /**
     * Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
     */
    private void Start()
    {
        harpoonCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;
    }

    /**
     * OnMouseDown is called when the user has pressed the mouse button while over the Collider.
     */
    private void OnMouseDown()
    {
        onDrag = true;
        initialPosition = GetMousePosition();
    }

    /**
     * OnMouseUp is called when the user has released the mouse button.
     */
    private void OnMouseUp()
    {
        onDrag = false;
    }

    /**
     * FixedUpdate is a frame-rate independent update method for physics calculations.
     */
    private void FixedUpdate()
    {
        if (onDrag && initialPosition != GetMousePosition())
        {
            RotateHarpoonWithMouse();
        }

        if (Input.touchCount > 0 && GetTouchIndexOnHarpoon() != -1)
        {
            RotateHarpoonWithTouch(GetTouchIndexOnHarpoon());
        }
    }

    /**
     * RotateHarpoonWithMouse rotates the harpoon to the vector between the first initial mouse position and the current mouse position.
     */
    private void RotateHarpoonWithMouse()
    {
        var direction = initialPosition - GetMousePosition();

        direction.Normalize();

        var rotationZ = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    /**
     * RotateHarpoonWithTouch rotates the harpoon with touch interactions
     * @param index is the index of the touchpoint that touches the harpoon
     */
    private void RotateHarpoonWithTouch(int index)
    {
        var touch = Input.GetTouch(index);

        // Handle finger movements based on touch phase.
        switch (touch.phase)
        {
            // Record initial touch position.
            case TouchPhase.Began:
                onDrag = true;
                initialPosition = touch.position;
                break;

            // Determine direction by comparing the current touch position with the initial one.
            case TouchPhase.Moved:
                var direction = initialPosition - new Vector3(touch.position.x, touch.position.y, 0f);

                direction.Normalize();

                var rotationZ = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;

                transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
                break;

            // Report that a direction has been chosen when the finger is lifted.
            case TouchPhase.Ended:
                onDrag = false;
                break;
        }
    }

    /**
     * GetMousePosition returns the current mouse position
     * @return Current mouse position
     */
    private Vector3 GetMousePosition()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    /**
     * GetTouchIndexOnHarpoon loops through all touchpoints to check if one is on the harpoon.
     */
    private int GetTouchIndexOnHarpoon()
    {
        var touchIndex = -1;
        for (var i = 0; i < Input.touchCount; ++i)
        {
            var touchPosition = mainCamera.ScreenToWorldPoint(Input.GetTouch(i).position);
            if (harpoonCollider.OverlapPoint(touchPosition))
            {
                touchIndex = i;
                break;
            }
        }

        return touchIndex;
    }
}