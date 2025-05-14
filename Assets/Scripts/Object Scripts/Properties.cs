using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Properties : MonoBehaviour
{
    public enum ObjectType
    {
        NONE,
        FOOD
    }
    public int weight = 0;
    public int nutrition = 0;
    public Guid id;
    public int objectId;
    public bool canStoreInInventory = false;

    public ObjectType objectType;
    public GameObject claimedBy;
    // Start is called before the first frame update
    void Start()
    {
        id = Guid.NewGuid();
        DefineObjectProperties();
    }

    // Update is called once per frame
    void Update()
    {
        if(objectId == 0)
        {
            DefineObjectProperties();
        }
    }

    private void DefineObjectProperties()
    {
        //Make sure that the propteties ids match the object creator ids
        switch(this.gameObject.name)
        {
            case "Wood Logs":
                weight = 45;
                objectId = 1;
                canStoreInInventory = true;
                break;
            case "Wood Logs(Clone)":
                weight = 45;
                objectId = 1;
                canStoreInInventory = true;
                break;
            case "CopperNuget(Clone)":
                objectId = 2;
                canStoreInInventory = true;
                break;
            case "Stone(Clone)":
                objectId = 3;
                canStoreInInventory = true;
                break;
            case "IronNugget(Clone)":
                objectId = 4;
                canStoreInInventory = true;
                break;
            case "Bed(Clone)":
                objectId = 5;
                canStoreInInventory = true;
                break;
            case "Shower(Clone)":
                objectId = 6;
                canStoreInInventory = true;
                break;
            case "Toliet(Clone)":
                objectId = 7;
                canStoreInInventory = true;
                break;
            case "IronNode":
                objectId = 8;
                canStoreInInventory = true;
                break;
            case "Tree(Clone)":
                objectId = 10;
                break;
            case "WaterTile(Clone)":
                objectId = 11;
                break;
            case "WheatSeed(Clone)":
                objectId = 12;
                canStoreInInventory = true;
                break;
            case "Wheat(Clone)":
                objectId = 13;
                canStoreInInventory = true;
                break;


        }
    }
}
