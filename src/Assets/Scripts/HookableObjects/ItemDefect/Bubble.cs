using UnityEngine;

/**
 * water bubble to put out fire of HarpoonDefect
 */
public class Bubble : MonoBehaviour
{
    private ItemDefect _parent;


    /**
     * get correpsonding ItemDefectHandler
     */
    private void Start()
    {
        _parent = GetComponentInParent<ItemDefect>();
    }


    /**
     * on collision with fire. Can only collide with fire as there are no other gameObjects in this collision layer.
     *
     * @param fire Collider2D of fireobjects
     */
    private void OnTriggerEnter2D(Collider2D fire)
    {
        if (fire.CompareTag("DefectFire")) // in case some players try to brake the game by colliding water bubbles
        {
            fire.gameObject.SetActive(false);
            //trigger check if fires are all put out
            _parent.CheckFireActive();
        }
    }

    /**
     * initialize bubble and return starting position
     *
     * @return returns initial position of bubble
     */
    public Vector3 InitBubble()
    {
        LeanHandler.MakeDraggable(this.gameObject);
        return gameObject.transform.position;
    }


    /**
     * sets position of bubble
     *
     * @param position Vector3D to set as position
     */
    public void SetPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }
}