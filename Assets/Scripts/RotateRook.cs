using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRook : MonoBehaviour
{
    public float rotSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotSpeed);
    }
}
