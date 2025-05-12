using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInentory : MonoBehaviour
{
    public GameObject inventoryPanel = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryPanel.SetActive(false);
        }
    }

    public void OnClick()
    {
        if(!inventoryPanel.active)
        {
            this.inventoryPanel.SetActive(true);
        }
    }
}
