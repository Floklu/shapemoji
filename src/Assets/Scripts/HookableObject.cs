using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HookableObject : MonoBehaviour
{

    private GameObject _parent;

    private Collider _collider;

    void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer("PlayingFieldLayer");
    }
    
    /**
     * Calls for action at controller on collision with
     *
     * @param Collider otherr
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        HookableObjectController.OnHookableObjectCollision(this, other.gameObject);
    }
}


public class Stone : HookableObject
{
    
}

public class Item : HookableObject
{
    
}