using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadMovement : MonoBehaviour
{
    public float speed = 0.006f;
    private float maxPos = 5;
    private float yContraction;

    [SerializeField]
    public Camera camera;
    void Start()
    {
        double y = 1 / Math.Sin((Math.PI / 180) * camera.transform.rotation.eulerAngles.x);
        yContraction = (float) y;
    }
    void Update()
    {
        var x = -Input.GetAxis("LHorizontal");
        var z = Input.GetAxis("LVertical");
        if (x != 0 || z != 0)
        {
            var padWorldPos = this.transform.position;
            var padCameraPos = camera.WorldToViewportPoint(padWorldPos);
            var cameraDiff = new Vector3(x, z * yContraction, camera.nearClipPlane) * speed;
            var padMovedCamera = padCameraPos + cameraDiff;
            var padWordMoved = camera.ViewportToWorldPoint(padMovedCamera);;
            padWordMoved.y = 0;

            this.transform.position = this.Clamp(padWordMoved, maxPos);
        }
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
