using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PadMovement : MonoBehaviour
{
    [SerializeField, Range(1.0f, 2.5f)]
    public float speed = 1.8f;

    private float maxPos = 4;
    private float yContraction;
    private int timeDoubled = 0;
    private GameObject text;

    [SerializeField]
    public new Camera camera;
    void Start()
    {
        double y = 1 / Math.Sin((Math.PI / 180) * camera.transform.rotation.eulerAngles.x);
        yContraction = (float)y;
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
            var padWordMoved = camera.ViewportToWorldPoint(padMovedCamera); ;
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

    public void DoubleScaleForTime(int seconds)
    {
        if (timeDoubled == 0)
        {
            text = CreateTextMeshes();
            this.transform.localScale *= 2;
            timeDoubled = seconds;
            ReturnToScale();
        }
        else
        {
            timeDoubled += seconds;
        }
    }

    private async void ReturnToScale()
    {
        SetTimeText(timeDoubled);
        timeDoubled--;
        if (timeDoubled > 0)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            ReturnToScale();
        }
        else
        {
            this.transform.localScale /= 2;
            Destroy(text);
        }
    }

    private void SetTimeText(int seconds)
    {
        text.GetComponent<TextMesh>().text = seconds.ToString();
    }

    private GameObject CreateTextMeshes()
    {
        GameObject textRoot = new GameObject("Text");
        var textMesh = textRoot.AddComponent<TextMesh>();
        textMesh.transform.SetParent(this.transform);
        textMesh.transform.localScale = new Vector3(0.5f, 1, 0.5f);
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.characterSize = 0.05f;
        textMesh.fontSize = 150;
        textMesh.transform.localRotation = Quaternion.Euler(90, 0, 0);
        textMesh.transform.localPosition = new Vector3(0, 0, 0);
        return textRoot;
    }
}
