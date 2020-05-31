using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public event EventHandler<BallEventArgs> BallFallen;
    void Start()
    {
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        BallFallen?.Invoke(this, new BallEventArgs(this.gameObject, other));
    }
}

public class BallEventArgs : EventArgs
{
    public BallEventArgs(GameObject gameObject, Collider other)
    {
        this.GameObject = gameObject;
        this.Collider = other;
    }

    public GameObject GameObject { get; }
    public Collider Collider { get; }
}

