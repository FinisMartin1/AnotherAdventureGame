using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMeterialMenu : MonoBehaviour
{
    public GameObject scrollMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (scrollMenu.active == true)
        {
            scrollMenu.SetActive(false);
        } else
        {
            scrollMenu.SetActive(true);
        }
    }
}
