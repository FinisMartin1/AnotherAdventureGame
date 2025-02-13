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
        switch(this.gameObject.name)
        {
            case "Wood Logs":
                weight = 45;
                objectId = 1;
                break;
            case "CopperNuget(Clone)":
                objectId = 2;
                break;
            case "Stone(Clone)":
                objectId = 3;
                break;
            case "IronNugget(Clone)":
                objectId = 4;
                break;
            case "Bed(Clone)":
                objectId = 5;
                break;
            case "Shower(Clone)":
                objectId = 6;
                break;
            case "Toliet(Clone)":
                objectId = 7;
                break;
            case "IronNode":
                objectId = 8;
                break;

        }
    }
}
