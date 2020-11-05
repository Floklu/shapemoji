using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    private float startRotationZ;

    private void FixedUpdate()
    {
        var difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        var rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (rotationZ > startRotationZ + 45f)
        {
            rotationZ = startRotationZ + 45f;
        }
        else if (rotationZ < startRotationZ - 45f)
        {
            rotationZ = startRotationZ - 45f;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }


    // Start is called before the first frame update
    void Start()
    {
        startRotationZ = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
    }
}