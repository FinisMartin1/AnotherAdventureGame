using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAssignJobMenu : MonoBehaviour
{
    public GameObject assignJobMenu;
    public GameObject selector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            assignJobMenu.SetActive(false);
            selector.SetActive(true);
        }
    }

    public void OnClick()
    {
        if(!assignJobMenu.active)
        {
            assignJobMenu.SetActive(true);
        }
    }
}
