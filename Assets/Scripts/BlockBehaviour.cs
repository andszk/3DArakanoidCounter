using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{
    private int count = 1;
    void Start()
    {
    }

    void Update()
    {   
    }

    void OnCollisionEnter(Collision other)
    {
        count--;
        if(count == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
