using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField]
    public Transform brick;

    //public float offset = 1.5f;
    public int numberOfBlocksX = 4;
    public int numberOfBlocksY = 5;

    private float maxPos = 4.5f;

    void Start()
    {
        var blockSize = brick.transform.localScale.x;
        var offsetX = ((2 * maxPos) - numberOfBlocksX * blockSize)/(numberOfBlocksX+1);
        var offsetY = ((2 * maxPos) - numberOfBlocksY * blockSize)/(numberOfBlocksY+1);       
        var startX = -maxPos + offsetX + blockSize / 2;
        var startY = -maxPos + offsetY + blockSize / 2;

        for (int i = 0; i < numberOfBlocksX; i++)
        {
            for (int j = 0; j < numberOfBlocksY; j++)
            {
                var position = new Vector3(startX + i* (blockSize + offsetX), 
                                           2,
                                           startY + j * (blockSize + offsetY));
                var cube = Instantiate(brick, position, Quaternion.identity);

                if (i==0 && j==0)
                {
                    cube.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
