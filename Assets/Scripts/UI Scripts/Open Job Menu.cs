using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{

    public GameObject woodCutMenu;
    public GameObject buildWallMenu;
    public GameObject mineMenu;
    public GameObject harvestPlantMenu;
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
        if(woodCutMenu.active || buildWallMenu.active || mineMenu.active)
        {
            woodCutMenu.SetActive(false);
            buildWallMenu.SetActive(false);
            mineMenu.SetActive(false);
            harvestPlantMenu.SetActive(false);
        }
        else
        {
            woodCutMenu.SetActive(true);
            buildWallMenu.SetActive(true);
            mineMenu.SetActive(true);
            harvestPlantMenu.SetActive(true);
        }
    }
}
