using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        this.rigidbody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Debug.Log("wohoo!!");
            this.transform.position = new Vector3(0, 0.5f, 0);
            this.rigidbody.velocity = new Vector3(Random.value, 5, Random.value);
        }
    }
}
