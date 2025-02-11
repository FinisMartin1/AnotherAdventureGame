using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    public enum HarvestType
    {
        tree,
        rock
    }

    public HarvestType harvestType = 0;
    public bool IsMarkedForHarvest = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
