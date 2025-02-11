using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINode : MonoBehaviour
{

    public List<UINode> UINodes = new List<UINode>();
    public string menuName = "";
    private bool selected = false;
    GameObject nodePrefab;
    NodeType nodeType = NodeType.Job;

    enum NodeType
    {
        Job
    }
    // Start is called before the first frame update
    void Start()
    {
        SetupNodes();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject selector = GameObject.Find("Select");
        if(selector != null && UINodes.Count != 0)
        {
            selector.TryGetComponent<SelectObject>(out SelectObject selectObject);
            if(selectObject != null && selectObject.selectedObjects.Contains(this.gameObject) )
            {
                if (!selected)
                {
                    if(UINodes.Count != 0)
                    {
                        CreateChildNodes();
                    }
                    else
                    {
                        switch (menuName)
                        {
                            case "Chop Tree Menu":
                                SetChopWoodAction();
                                
                                break;
                        }
                    }
                    selected = true;
                }
                else
                {
                    CloseChildNodes();
                    selected = false;
                }
                selectObject.selectedObjects.Remove(this.gameObject);
            }
        }
    }

    private void CreateChildNodes()
    {
        int count = 1;
        foreach (UINode node in UINodes)
        {
            
            GameObject gameObject = new GameObject();
            Debug.Log(this.gameObject.transform.position);
            gameObject.AddComponent<SpriteRenderer>();
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("UI_Panel_Plane");
            gameObject.AddComponent<UINode>();
            gameObject.GetComponent<UINode>().menuName = node.menuName;
            gameObject.name = node.menuName;
            gameObject.AddComponent<BoxCollider2D>();
            gameObject.transform.SetParent(GameObject.Find("UI Manager").transform);
            gameObject.transform.localScale = new Vector3(100, 100, 100);
            gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + (float)(2 * count), 0);
            gameObject.tag = "UI";
            count++;
        }

    }

    private void CloseChildNodes()
    {
        foreach (UINode node in UINodes)
        {
            GameObject gameObject = GameObject.Find(node.menuName);
            Destroy(gameObject);
        }
    }

    private void SetupNodes()
    {
        if (menuName == "")
        {

            switch (nodeType)
            {
                case NodeType.Job:
                    UINode node = new UINode();
                    node.menuName = "Chop Tree Menu";
                    this.UINodes.Add(node);
                    node = new UINode();
                    node.menuName = "Build Menu";
                    this.UINodes.Add(node);
                    break;
            }
        }  
    }

    private void SetChopWoodAction()
    {
        GameObject selector = GameObject.Find("Select");
        selector.GetComponent<SelectObject>().SetActionSelector(SelectObject.ActionSelector.ChopWood);
        selector.GetComponent<SelectObject>().SetSelector(SelectObject.Selectors.InRectangle);
    }
}
