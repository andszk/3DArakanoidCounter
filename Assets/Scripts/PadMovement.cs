using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadMovement : MonoBehaviour
{
    [SerializeField, Range(1.0f, 2.5f)]
    public float speed = 1.8f;

    private float maxPos = 4;
    private float yContraction;

    [SerializeField]
    public new Camera camera;
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
            var cameraDiff = new Vector3(x, z * yContraction, camera.nearClipPlane) * speed * Time.unscaledDeltaTime;
            var padMovedCamera = padCameraPos + cameraDiff;
            var padWordMoved = camera.ViewportToWorldPoint(padMovedCamera);;
            padWordMoved.y = 0;

            this.transform.position = this.Clamp(padWordMoved, maxPos);
        }
    }

    private Vector3 Clamp(Vector3 vec, float clamp)
    {
        vec.x = Mathf.Clamp(vec.x, -clamp, clamp);
        vec.z = Mathf.Clamp(vec.z, -clamp, clamp);
        return vec;
    }
}
