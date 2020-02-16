using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadMovement : MonoBehaviour
{
    public float speed = 0.1f;
    private float maxPos = 5;

    [SerializeField]
    public Camera camera;
    void Start()
    {
        
    }
    void Update()
    {
        var x = Input.GetAxis("LHorizontal") * speed;
        var z = Input.GetAxis("LVertical") * speed;
        var movement = new Vector3(x, 0, z);
        //var pos = camera.transform.TransformDirection(movement);
        var pos = camera.ScreenToWorldPoint(this.transform.position + movement);
        pos.y = 0;

        var position = this.transform.position + pos;
        this.transform.position = this.Clamp(position, maxPos);
    }

    private Vector3 Clamp(Vector3 vec, float clamp)
    {
        if (Math.Abs(vec.x) > clamp)
        {
            vec.x = vec.normalized.x * clamp;
        }

        if (Math.Abs(vec.z) > clamp)
        {
            vec.z = vec.normalized.z * clamp;
        }

        return vec;
    }
}
