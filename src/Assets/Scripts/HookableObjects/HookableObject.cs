using UnityEngine;

/**
 * abstract class HookableObject is parent class of all GameObject which can be hooked by projectile
 *
 *  */
public abstract class HookableObject : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    protected GameObject Parent;

    /**
     * on Start() set layer to PlayingFieldLayer
     */
    protected virtual void Start()
    {
        SetLayerToPlayingFieldLayer();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    /**
     * Calls for action at controller on collision with
     *
     * @param Collider other
     */
    public abstract void OnTriggerEnter2D(Collider2D other);


    /**
     * sets order in layer
     *
     * @param order Int to be set as order in layer
     */
    public void SetOrderInLayer(int order)
    {
        _spriteRenderer.sortingOrder = order;
    }

    /**set color of renderer
     *
     * @param color Color to set
     */
    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    /**
 * get current position on playing field
 */
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    /**
         * change collision layer to PlayingFieldLayer
         */
    public void SetLayerToPlayingFieldLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayingFieldLayer");
    }

    /**
         * change collision layer to DraggableLayer
         */
    public void SetLayerToDraggableLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("DraggableLayer");
    }

    /**
     * sets transform parent so movement is joined
     */
    public void SetTransformParent(Transform parentTransform)
    {
        transform.parent = parentTransform;
    }

    /**
     * SetParent sets the parent of this gameobject
     *
     * @param GameObject parent: parent to set
     */
    public void SetParent(GameObject parent)
    {
        Parent = parent;
    }

    /**
     * set new parent and return old parent
     *
     * @returns parent before changing
     * @param newParent is the parent given to SetParent
     */
    public GameObject ChangeParent(GameObject newParent)
    {
        var oldParent = Parent;
        SetParent(newParent);
        return oldParent;
    }

    /**
     * SetPosition sets the position of the hookable object
     *
     * @param position The position to set the hookable object to
     */
    public void SetPosition(Vector3 position)
    {
        //gameObject.transform.position = position;
        SmoothTransition.AddTransition(gameObject, position, 0.25f);
    }

    /**
     * DestroyHookableObject destroys the hookable object
     */
    public void DestroyHookableObject()
    {
        Destroy(gameObject);
    }

    /**
     * OnWoundIn is called when the Harpoon is wound in
     */
    public abstract void OnWoundIn(Inventory inventory);


    /**
     * activates or deactivades Collider2D
     *
     * @param state bool to set Collider2D.enabled
     */
    public void SetColliderState(bool state)
    {
        var _collider = GetComponent<PolygonCollider2D>();
        _collider.enabled = state;
    }
}