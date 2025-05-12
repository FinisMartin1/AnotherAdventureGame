using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignJob : MonoBehaviour
{
    public GameObject selector;
    public GameObject selectedPawn = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(selector.GetComponent<SelectObject>().selectedPlayer != null)
        {
            selectedPawn = selector.GetComponent<SelectObject>().selectedPlayer;
        }
        if(selectedPawn != null)
        {
            switch(this.gameObject.name)
            {
                case "BuilderToggle":
                    this.gameObject.GetComponent<Toggle>().isOn = selectedPawn.GetComponent<Jobs>().IsBuilder;
                    break;
                case "MinerToggle":
                    this.gameObject.GetComponent<Toggle>().isOn = selectedPawn.GetComponent<Jobs>().IsMiner;
                    break;
                case "WoodCutterToggle":
                    this.gameObject.GetComponent<Toggle>().isOn = selectedPawn.GetComponent<Jobs>().IsWoodCutter;
                    break;
                case "HaulerToggle":
                    this.gameObject.GetComponent<Toggle>().isOn = selectedPawn.GetComponent<Jobs>().IsHualer;
                    break;
                case "FarmerToggle":
                    this.gameObject.GetComponent<Toggle>().isOn = selectedPawn.GetComponent<Jobs>().IsFarmer;
                    break;
            }
        }    
    }

    public void OnValueChange()
    {
        if(this.gameObject.GetComponent<Toggle>().isOn)
        {
            switch (this.gameObject.name)
            {
                case "BuilderToggle":
                    selectedPawn.GetComponent<Jobs>().IsBuilder = true;
                    break;
                case "MinerToggle":
                    selectedPawn.GetComponent<Jobs>().IsMiner = true;
                    break;
                case "WoodCutterToggle":
                    selectedPawn.GetComponent<Jobs>().IsWoodCutter = true;
                    break;
                case "HaulerToggle":
                    selectedPawn.GetComponent<Jobs>().IsHualer = true;
                    break;
                case "FarmerToggle":
                    selectedPawn.GetComponent<Jobs>().IsFarmer = true;
                    break;
            }
        }
        else
        {
            switch (this.gameObject.name)
            {
                case "BuilderToggle":
                    selectedPawn.GetComponent<Jobs>().IsBuilder = false;
                    break;
                case "MinerToggle":
                    selectedPawn.GetComponent<Jobs>().IsMiner = false;
                    break;
                case "WoodCutterToggle":
                    selectedPawn.GetComponent<Jobs>().IsWoodCutter = false;
                    break;
                case "HaulerToggle":
                    selectedPawn.GetComponent<Jobs>().IsHualer = false;
                    break;
                case "FarmerToggle":
                    selectedPawn.GetComponent<Jobs>().IsFarmer = false;
                    break;
            }
        }
    }
}
