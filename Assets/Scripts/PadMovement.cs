using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadMovement : MonoBehaviour
{
    public float speed = 0.008f;
    private float maxPos = 5;

    [SerializeField]
    public Camera camera;
    void Start()
    {
        
    }
    void Update()
    {
        var x = Input.GetAxis("LHorizontal");
        var z = Input.GetAxis("LVertical");
        var cameraDiff = new Vector3(-x, z) * speed;
        var padWorldPos = this.transform.position;
        var padCameraPos = camera.WorldToViewportPoint(padWorldPos);
        var padMovedCamera = padCameraPos + cameraDiff;
        var padWordMoved = camera.ViewportToWorldPoint(padMovedCamera);
        padWordMoved.y = 0;

        this.transform.position = this.Clamp(padWordMoved, maxPos);
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
