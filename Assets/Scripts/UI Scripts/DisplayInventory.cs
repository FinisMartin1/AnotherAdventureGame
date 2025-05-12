using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayInvetory : MonoBehaviour
{
    public GameObject player = null;
    public GameObject selector = null;
    public GameObject inventoryNodePrefab = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = this.selector.GetComponent<SelectObject>().selectedPlayer;
        if(this.gameObject.active && player != null)
        {
            CreateInventoryNodes(player.GetComponent<Inventory>().invetory);
        }
    }

    private void CreateInventoryNodes(List<GameObject> gameObjects)
    {
        foreach(GameObject gameObject in gameObjects)
        {
            GameObject inventoryNode = new GameObject();
            inventoryNode = Instantiate(inventoryNodePrefab);
            inventoryNode.GetComponent<TextMeshPro>().SetText(gameObject.name);
            inventoryNode.transform.parent = this.transform;
        }
    }
}
