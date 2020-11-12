using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool _stop;

    private bool _isShot;

    private GameObject _harpoon;

    private Rigidbody2D _projectileRigidBody2D;

    [SerializeField] private float projectileSpeed = 500;

    // Start is called before the first frame update
    private void Start()
    {
        _projectileRigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_stop && _isShot)
        {
            _projectileRigidBody2D.velocity = gameObject.transform.right * projectileSpeed;
        }
        else
        {
            _projectileRigidBody2D.velocity = gameObject.transform.right * 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _stop = true;
    }

    public void Shoot()
    {
        _isShot = true;
    }
}