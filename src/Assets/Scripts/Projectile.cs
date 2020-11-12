using UnityEngine;

/**
 * Class Projectile contains the functionality for the harpoon projectile.
 */
public class Projectile : MonoBehaviour
{
    private bool _stop;

    private bool _isShot;

    private GameObject _harpoon;

    private Rigidbody2D _projectileRigidBody2D;

    [SerializeField] private float projectileSpeed = 500;

    /**
     * Start is called before the first frame update and sets the rigidbody of the projectile.
     */
    private void Start()
    {
        _projectileRigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    /**
     * Update is called once per frame.
     */
    private void Update()
    {
        if (!_stop && _isShot)
        {
            _projectileRigidBody2D.velocity = gameObject.transform.right * projectileSpeed;
        }
        else if (_stop)
        {
            _projectileRigidBody2D.velocity = Vector2.zero;
        }
    }

    /**
     * OnTriggerEnter2D is called when the collider enters a trigger
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        _stop = true;
    }

    /**
     * Shoot sets the _isShot boolean true so velocity can be applied to the projectile in the Update method
     */
    public void Shoot()
    {
        _isShot = true;
    }
}