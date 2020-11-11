using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonRope : MonoBehaviour
{
    public GameObject projectile;
    public GameObject cannon;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 centerPos = (cannon.transform.position + projectile.transform.position) / 2f;
        transform.position = centerPos;
        Vector3 scale = transform.localScale;
        //scale.x = Vector3.Distance(cannon.transform.position,projectile.transform.position)/ GetComponent<SpriteRenderer>().bounds.size.x;
        scale.x *= projectile.transform.position.x / _spriteRenderer.bounds.extents.x;

        if (scale.x < 0)
        {
            scale.x *= -1;
        }

    
        //transform.localScale = scale;
    }
}
