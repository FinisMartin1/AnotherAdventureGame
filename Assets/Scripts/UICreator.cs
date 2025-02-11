using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICreator : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        CreateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateUI()
    {
        CreateJobUI();
    }

    private void CreateJobUI()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
  
        GameObject gameObject = new GameObject();
        Rect rect = this.GetComponent<RectTransform>().rect;
        Debug.Log(this.GetComponent<RectTransform>().anchorMin);
        gameObject.transform.position = new Vector3(rect.height, rect.width);
        gameObject.transform.localScale = new Vector3(2, 2, 2);
        gameObject.AddComponent<SpriteRenderer>();
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("UI_Panel_Plane");
        gameObject.AddComponent<UINode>();
        gameObject.GetComponent<UINode>().menuName = "Job Menu";
        gameObject.AddComponent<BoxCollider2D>();

    }
}
