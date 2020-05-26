using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField, Range(5f, 25f)]
    public float speed = 10;

    private Rigidbody rigidbody;
    void Start()
    {
        this.rigidbody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            this.transform.position = new Vector3(0, 0.5f, 0);
            this.rigidbody.velocity = new Vector3(Random.value, 1, Random.value);
        }
    }

    void LateUpdate()
    {
        rigidbody.velocity = speed * (rigidbody.velocity.normalized);
    }

    void OnTriggerEnter(Collider other)
    {
        //Destroy(this.gameObject);
    }
}
