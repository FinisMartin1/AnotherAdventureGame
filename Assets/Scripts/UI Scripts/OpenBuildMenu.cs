using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBuildMenu : MonoBehaviour
{
    public GameObject buildMenu;
    public GameObject Select;
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
        if (buildMenu.active || buildMenu.active)
        {
            buildMenu.SetActive(false);
            Select.SetActive(true);
        }
        else
        {
            buildMenu.SetActive(true);
        }
    }
}
