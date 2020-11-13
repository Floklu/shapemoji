using System;
using UnityEngine;

/**
 * Class Harpoon contains functionality for the rotation and shooting of the harpoon.
 */
public class Harpoon : MonoBehaviour
{
    private bool _onDrag;

    private Vector3 _initialPosition;

    private Camera _mainCamera;

    private Collider2D _harpoonCollider;

    private GameObject _projectile;

    public GameObject harpoonRope;
    
    private bool _isShot;


    /**
     * Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
     */
    private void Start()
    {
        _harpoonCollider = GetComponent<Collider2D>();
        _mainCamera = Camera.main;
        _projectile = gameObject.transform.Find("HarpoonCannon/HarpoonProjectile").gameObject;

        harpoonRope.SetActive(false);
    }

    /**
     * OnMouseDown is called when the user has pressed the mouse button while over the Collider.
     */
    private void OnMouseDown()
    {
        if (!_isShot)
        {
            _onDrag = true;
            _initialPosition = GetMousePosition();
        }
    }

    /**
     * OnMouseUp is called when the user has released the mouse button.
     */
    private void OnMouseUp()
    {
        _onDrag = false;
        _isShot = true;
        _projectile.GetComponent<Projectile>().Shoot();
        harpoonRope.SetActive(true);
    }

    /**
     * FixedUpdate is a frame-rate independent update method for physics calculations.
     */
    private void FixedUpdate()
    {
        if (_onDrag && _initialPosition != GetMousePosition())
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
        var direction = _initialPosition - GetMousePosition();

        direction.Normalize();

        var rotationZ = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    /**
     * RotateHarpoonWithTouch rotates the harpoon with touch interactions
     * @param index is the index of the touch point that touches the harpoon
     */
    private void RotateHarpoonWithTouch(int index)
    {
        var touch = Input.GetTouch(index);

        // Handle finger movements based on touch phase.
        switch (touch.phase)
        {
            // Record initial touch position.
            case TouchPhase.Began:
                _onDrag = true;
                _initialPosition = touch.position;
                break;

            // Determine direction by comparing the current touch position with the initial one.
            case TouchPhase.Moved:
                var direction = _initialPosition - new Vector3(touch.position.x, touch.position.y, 0f);

                direction.Normalize();

                var rotationZ = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;

                transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
                break;

            // Report that a direction has been chosen when the finger is lifted.
            case TouchPhase.Ended:
                _onDrag = false;
                break;
            case TouchPhase.Stationary:
                break;
            case TouchPhase.Canceled:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /**
     * GetMousePosition returns the current mouse position
     * @return Current mouse position
     */
    private Vector3 GetMousePosition()
    {
        return _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    /**
     * GetTouchIndexOnHarpoon loops through all touch points to check if one is on the harpoon.
     * @return index of the touch that is on the harpoon
     */
    private int GetTouchIndexOnHarpoon()
    {
        var touchIndex = -1;
        for (var i = 0; i < Input.touchCount; ++i)
        {
            var touchPosition = _mainCamera.ScreenToWorldPoint(Input.GetTouch(i).position);
            if (_harpoonCollider.OverlapPoint(touchPosition))
            {
                touchIndex = i;
                break;
            }
        }

        return touchIndex;
    }
}