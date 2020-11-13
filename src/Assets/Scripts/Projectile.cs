using UnityEngine;
using Spawner;
using UnityEditor;

/**
 * Class Projectile contains the functionality for the harpoon projectile.
 */
public class Projectile : MonoBehaviour
{
    private bool _stop;

    private bool _isShot;

    private bool _hasGameObjHooked;

    private GameObject _gameObjectHooked;

    private StoneSpawner _stoneSpawner;

    private GameObject _harpoon;

    private GameObject _cannon;

    private Rigidbody2D _projectileRigidBody2D;

    private Color _color;

    [SerializeField] private float projectileSpeed = 500;

    /**
     * Start is called before the first frame update and sets the rigidbody of the projectile.
     */
    private void Start()
    {
        _stoneSpawner = GameObject.Find("StoneSpawner")?.GetComponent<StoneSpawner>();
        //get parent and its color
        _cannon = gameObject.transform.parent.gameObject;
        _color = _cannon.GetComponent<Renderer>().material.color;
        _projectileRigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        _hasGameObjHooked = false; //on start no object is hooked
 
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
     *
     * Gets called on collision of Projectile with GameObj other. Stops projectile and 
     * hooks GameObj if stone or item.
     *
     * @param other Other Collider that got hit.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        //TODO: remove comment when rewinding is implemented
        //uncomment to test attaching until rewinding is implemented
        _stop = true;
        if (other != null) // and if hookable
        {
            if (other.tag.Equals("Stone")) //hook stone and clear spot in spawner
            {
                AttachObject(other.gameObject);
            }
            //TODO: attach items via else if when items created
        }
    }

    /**
     * Shoot sets the _isShot boolean true so velocity can be applied to the projectile in the Update method
     */
    public void Shoot()
    
    {
        _isShot = true;
    } 
 
    /**
     *  sets parent to be classes gameObject
     *
     * @ param child object set to be child of gameObject 
    */
    public void AttachObject(GameObject child)
    {
        _gameObjectHooked = child;
        _gameObjectHooked.transform.parent = gameObject.transform;
        _hasGameObjHooked = true;
        _cannon.GetComponent<Renderer>().material.color = Color.green;
    }
    
    /**
     * removes parent of _gameObjectHooked and therefore unattaches it 
     *
     */
    public void UnattachObject()
    {
        if (!_hasGameObjHooked) return;
        _cannon.GetComponent<Renderer>().material.color = _color;
        _gameObjectHooked.transform.parent = null;
        _hasGameObjHooked = false;
        _stoneSpawner.DeleteStone(_gameObjectHooked);

    }

    /**
     * sets boolean _stop
     *
     * @param status value _stop is set to
     */
    public void set_stop(bool status)
    {
        _stop = status;
    }
}