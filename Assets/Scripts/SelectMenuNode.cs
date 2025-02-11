using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenuNode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<TMPro.TextMeshPro>();
        gameObject.AddComponent<UnityEngine.Rendering.SortingGroup>();
        gameObject.GetComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = 6;
        gameObject.transform.position = this.gameObject.transform.position;
        Vector3 position = gameObject.transform.position;
        position.x = position.x - 10;
        position.y = position.y - 5;
        gameObject.transform.position = position;
        gameObject.GetComponent<TMPro.TextMeshPro>().text = "Chop Tree";
        gameObject.GetComponent<TMPro.TextMeshPro>().fontSize = 26;
        gameObject.transform.parent = this.gameObject.transform;

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
