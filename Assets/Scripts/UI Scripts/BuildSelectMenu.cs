using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuildSelectMenu : MonoBehaviour
{
    public GameObject selectMenuNodePrefab = null;
    public GameObject selector = null;
    public bool menuBuilt = false;

    private List<GameObject> menuNodes = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selector.GetComponent<SelectObject>().selectMenuObject != null && !menuBuilt)
        {
            CreateMenu();
        }
    }

    public void CloseMenu()
    {
       foreach(GameObject menuNode in menuNodes)
        {
            Destroy(menuNode);
        }
        menuNodes.Clear();
        menuBuilt = false;
        selector.GetComponent<SelectObject>().selectMenuObject = null;
    }

    private void CreateMenu()
    {
        
        if (selector.GetComponent<SelectObject>().selectMenuObject.GetComponent<Properties>() != null && selector.GetComponent<SelectObject>().selectMenuObject.GetComponent<Properties>().canStoreInInventory && selector.GetComponent<SelectObject>().selectedPlayer != null)
        {
            GameObject menuNode = Instantiate(selectMenuNodePrefab);
            menuNode.GetComponent<SelectMenuControl>().selectMenuControlType = SelectMenuControl.SelectMenuControlType.PICKUPITEM;

            menuNode.transform.position = Utils.GetMouseWorldPostion();
            menuNode.GetComponent<SelectMenuControl>().selector = selector;
            menuNode.GetComponent<SelectMenuControl>().player = selector.GetComponent<SelectObject>().selectedPlayer;
            menuNode.transform.parent = this.gameObject.transform;
            menuNodes.Add(menuNode);
        }
        menuBuilt = true;
    }
    
}
