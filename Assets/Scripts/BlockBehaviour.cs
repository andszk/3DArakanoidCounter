using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BlockBehaviour : MonoBehaviour
{
    private int count = 4;
    private List<TextMesh> textMeshes = new List<TextMesh>();

    void Start()
    {
        this.CreateTextMeshes(this.transform.localScale.x);
    }

    void Update()
    {   
    }

    void OnCollisionEnter(Collision other)
    {
        count--;
        textMeshes.ToList().ForEach(mesh => mesh.text = count.ToString());
        if(count == 0)
        {
            Destroy(this.gameObject);
        }
    }

    void CreateTextMeshes(float scale)
    {
        
        for (int i = 0; i < 4; i++)
        {
            GameObject textRoot = new GameObject("Text");
            textRoot.transform.SetParent(this.transform);
            textMeshes.Add(textRoot.AddComponent<TextMesh>());
            textMeshes[i].transform.SetParent(textRoot.transform);

            textMeshes[i].text = count.ToString();
            textMeshes[i].anchor = TextAnchor.MiddleCenter;
            //for clear rendering, characterSize=1 is pixelated
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
