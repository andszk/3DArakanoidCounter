using System;
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

    internal void TripleBalls()
    {
        List<Transform> newBalls = new List<Transform>();

        // Add two new balls for each existing
        for (int i = 0; i < 2; i++)
        {
            foreach (var oldBall in balls)
            {
                var ball = InitializeBall();
                ball.position = oldBall.position;
                newBalls.Add(ball);
            }
        }

        balls.AddRange(newBalls);
    }

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            balls.Add(InitializeBall());
        }
    }

    void LateUpdate()
    {
        foreach (var ball in balls)
        {
            var rigidbody = ball.GetComponent<Rigidbody>();
            rigidbody.velocity = speed * (rigidbody.velocity.normalized);
        }

    }

    private Transform InitializeBall()
    {
        var ball = Instantiate(ballPrefab);
        ball.GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.value, 1, UnityEngine.Random.value);
        ball.transform.position = new Vector3(0, 0.5f, 0);
        ball.GetComponent<Ball>().BallFallen += HandleBallFallen;
        ball.SetParent(this.transform);

        return ball;
    }

    private void HandleBallFallen(object sender, BallEventArgs e)
    {
        var ball = balls.Single(b => b.gameObject == e.GameObject);
        balls.Remove(ball);
        Destroy(ball.gameObject);
    }
}
