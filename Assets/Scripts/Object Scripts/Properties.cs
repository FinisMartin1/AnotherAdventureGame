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
        
    }

    private void DefineObjectProperties()
    {
        switch(this.gameObject.name)
        {
            case "Wood Logs":
                weight = 45;
                objectId = 1;
                break;
            case "CopperNuget":
                objectId = 2;
                break;
            case "Stone":
                objectId = 3;
                break;
            case "IronNuget":
                objectId = 4;
                break;
            case "Bed":
                objectId = 5;
                break;
            case "Shower":
                objectId = 6;
                break;
            case "Toliet":
                objectId = 7;
                break;
        }
    }
}
