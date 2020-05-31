using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PowerUpComponent : MonoBehaviour
{
    [SerializeField, Range(5f, 25f)]
    public float velocity = 10;

    private PowerUp powerUp;

    void Start()
    {
        powerUp = PowerUpFactory.GetRandomPowerUp();
    }

    void Update()
    {
        var displacement = new Vector3(0, velocity * Time.unscaledDeltaTime, 0);
        this.transform.position -= displacement;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.other.name == "Pad")
        {
            powerUp.ResolveEffect();
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
