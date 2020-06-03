using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BlockBehaviour : MonoBehaviour
{
    [SerializeField]
    public Transform powerUpPrefab;

    [SerializeField, Range(0, 100)]
    public int dropChance = 30;

    private int count = 4;
    private List<TextMesh> textMeshes = new List<TextMesh>();
    private static System.Random rnd = new System.Random();

    void Start()
    {
        this.CreateTextMeshes(this.transform.localScale.x);
    }

    void Update()
    {   
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.name.Contains("Ball"))
        {
            count--;
            GeneratePowerUp();
            textMeshes.ToList().ForEach(mesh => mesh.text = count.ToString());
            if (count == 0)
            {
                this.transform.parent.GetComponent<BlockManager>().Cubes.Remove(this.transform);
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        this.transform.parent.GetComponent<BlockManager>().Cubes.Remove(this.transform);
        Destroy(this.gameObject);
    }

    private void GeneratePowerUp()
    {
        if (rnd.Next(0,100) < dropChance)
        {
            var powerUp = Instantiate(powerUpPrefab);
            powerUp.transform.position = this.transform.position;
        }
    }

    private void CreateTextMeshes(float scale)
    {
        
        for (int i = 0; i < 4; i++)
        {
            GameObject textRoot = new GameObject("Text");
            textRoot.transform.SetParent(this.transform);
            textMeshes.Add(textRoot.AddComponent<TextMesh>());
            textMeshes[i].transform.SetParent(textRoot.transform);

            textMeshes[i].text = count.ToString();
            textMeshes[i].anchor = TextAnchor.MiddleCenter;
            //for clear rendering, with standard characterSize=1 font is pixelated
            textMeshes[i].characterSize = 0.05f;
            textMeshes[i].fontSize = 150;
        }

        textMeshes[0].transform.localRotation = Quaternion.Euler(0, 90, 0);
        textMeshes[0].transform.localPosition = new Vector3(-scale / 2, 0, 0);

        textMeshes[1].transform.localRotation = Quaternion.Euler(0, 180, 0);
        textMeshes[1].transform.localPosition = new Vector3(0, 0, scale/2);

        textMeshes[2].transform.localRotation = Quaternion.Euler(0, 270, 0);
        textMeshes[2].transform.localPosition = new Vector3(scale / 2, 0, 0);

        textMeshes[3].transform.localRotation = Quaternion.Euler(0, 0, 0);
        textMeshes[3].transform.localPosition = new Vector3(0, 0, -scale/2);
    }
}
