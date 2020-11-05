using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Harpoon : MonoBehaviour
{
    [SerializeField] private float startRotationZ;
    [SerializeField] private float harpoonRange = 45f;

    private bool onHarpoon;

    // Start is called before the first frame update
    void Start()
    {
        var eulerAngle = transform.eulerAngles.z;
        startRotationZ = transform.eulerAngles.z;
    }


    private void OnMouseDown()
    {
        onHarpoon = true;
    }

    private void OnMouseUp()
    {
        onHarpoon = false;
    }

    private void FixedUpdate()
    {
        if (onHarpoon)
        {
            var difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            difference.Normalize();

            // // TODO: dont hardcode 45f 
            // var rotationZ = nfmod(((Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg) - 45f), 360);
            var rotationZ = (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg) - 45f;

            // if (rotationZ > nfmod(startRotationZ + harpoonRange, 360))
            // {
            //     rotationZ = nfmod(startRotationZ + harpoonRange, 360);
            // }
            // else if (rotationZ < nfmod(startRotationZ - harpoonRange, 360))
            // {
            //     rotationZ = nfmod(startRotationZ - harpoonRange, 360);
            // }

            Debug.Log(rotationZ);

            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }
    }

    float nfmod(float a, float b)
    {
        return (float) (a - b * Math.Floor(a / b));
    }


    // Update is called once per frame
    void Update()
    {
    }
}