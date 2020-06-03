using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField, Range(5f, 25f)]
    public float speed = 10;

    [SerializeField]
    public Transform ballPrefab;

    private List<Transform> balls = new List<Transform>();
    private bool pullObjects = false;
    private List<Rigidbody> objectsToPull 
    { 
        get
        {
            var cubes = FindObjectOfType<BlockManager>().Cubes;
            return cubes.Select(cube => { return cube?.GetComponent<Rigidbody>(); }).ToList();
        }
        set {} 
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

        if (pullObjects)
        {
            PullEachObject();
        }
    }

    void LateUpdate()
    {
        if (!pullObjects)
        {
            foreach (var ball in balls)
            {
                var rigidbody = ball.GetComponent<Rigidbody>();
                rigidbody.velocity = speed * (rigidbody.velocity.normalized);
            }
        }
    }

    public void TripleBalls()
    {
        List<Transform> newBalls = new List<Transform>();

        if (balls.Count >= 300)
        {
            return;
        }

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

    public async void GravityPull()
    {
        // effect alfready in change, do nothing
        if (pullObjects)
        {
            return;
        }

        objectsToPull.ForEach(cube => cube.isKinematic = false);
        pullObjects = true;

        await Task.Delay(TimeSpan.FromSeconds(2));

        pullObjects = false;
        objectsToPull.ForEach(cube => cube.isKinematic = true);
    }

    private void PullEachObject()
    {
        var ballsRigidbodies = balls.Select(cube => { return cube.GetComponent<Rigidbody>(); }).ToList();

        foreach (var ball in ballsRigidbodies)
        {
            if (ball == null) break;

            var rigidbodies = ballsRigidbodies.Where(b => b!=ball).ToList();
            rigidbodies.AddRange(objectsToPull);

            foreach (var pull in rigidbodies)
            {
                if (pull == null) break;
                float r = Vector3.Distance(ball.position, pull.position);
                if (r == 0) break;
                var force = (ball.position - pull.position) / (r * r);
                pull.AddForce(force, ForceMode.Force);
                ball.AddForce(-force, ForceMode.Force);
            }
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
