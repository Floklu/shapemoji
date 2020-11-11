using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Rope Lengthening Class
 * @Author Andrei Dziubenka
 * @Date 11.11.2020
 */
public class HarpoonRope : MonoBehaviour
{
    public GameObject projectile;
    public GameObject cannon;
    
    private SpriteRenderer _spriteRenderer;

    private Transform _ropeTransform;
    private Transform _cannonTransform;
    private Transform _projectileTransform;

    /**
     * Start Method is used to initialize variables
     */
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _ropeTransform = transform;
        _cannonTransform = cannon.transform;
        _projectileTransform = projectile.transform;
    }

    /**
     * Update Method is called every frame
     */
    void Update()
    {
        
        Vector3 ropeCenter = (cannon.transform.position + projectile.transform.position) / 2f;
        
        _ropeTransform.position = ropeCenter;
        Vector3 ropeScale = _ropeTransform.localScale;

        float requiredRopeLength = Vector3.Distance(_cannonTransform.position,_projectileTransform.position);
        
        float currentRopeLength = _spriteRenderer.bounds.size.x;

        ropeScale.x *= requiredRopeLength / currentRopeLength;
        transform.localScale = ropeScale;
    }
}
