using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField, Range(5f, 25f)]
    public float speed = 10;

    [SerializeField]
    public Transform ballPrefab;

    private List<Transform> balls = new List<Transform>();
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            var ball = Instantiate(ballPrefab);

            ball.GetComponent<Rigidbody>().velocity = new Vector3(Random.value, 1, Random.value);
            ball.transform.position = new Vector3(0, 0.5f, 0);
            ball.GetComponent<Ball>().BallFallen += HandleBallFallen;
            ball.SetParent(this.transform);
            balls.Add(ball);
        }
    }

    void LateUpdate()
    {
        foreach(var ball in balls)
        {
            var rigidbody = ball.GetComponent<Rigidbody>();
            rigidbody.velocity = speed * (rigidbody.velocity.normalized);
        }
        
    }

    private void HandleBallFallen(object sender, BallEventArgs e)
    {
        var ball = balls.Single(b => b.gameObject == e.GameObject);
        balls.Remove(ball);
        Destroy(ball.gameObject);
    }
}
