using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Farmplot : MonoBehaviour
{
    public enum SeedType
    {
        NONE,
        WHEET
    }
    public bool readyForHarvest = false;
    public bool hasSeed = false;
    public SeedType seed = SeedType.NONE;
    private GameObject sun = null;
    private float maturity = 0;
    private float growthRate = 0;
    public GameObject objectCreator = null;
    // Start is called before the first frame update
    void Start()
    {
        if(objectCreator == null)
        {
            objectCreator = Utils.GetAllGameObjects().First(g => g.name == "Object Creator");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(sun == null)
        {
            sun = Utils.GetAllGameObjects().First(g => g.GetComponent<DayNightCycle>() != null);
        }
        
        if(seed != SeedType.NONE && sun != null && hasSeed)
        {
            SetGrowthRate();
        }
        
        if(growthRate>0)
        {
            maturity = maturity + growthRate;
        }

        if(maturity>=100)
        {
            readyForHarvest = true;
            maturity = 100;
        }
    }

    public void ProducePlantAndSeed()
    {
        switch(seed)
        {
            case SeedType.WHEET:
                objectCreator.GetComponent<ObjectCreator>().createObject(13, this.gameObject.transform.position);
                objectCreator.GetComponent<ObjectCreator>().createObject(12, this.gameObject.transform.position);
                break;
        }
        hasSeed = false;
        maturity = 0;
        growthRate = 0;
        readyForHarvest = false;
        this.GetComponent<Harvestable>().IsMarkedForHarvest = false;
    }
    public int GetSeedId(SeedType seedType)
    {
        switch(seedType)
        {
            case SeedType.WHEET:
                return 12;
        }
        return -1;
    }

    private void SetGrowthRate()
    {
        int multiplier = 0;

        switch(seed)
        {
            case SeedType.WHEET:
                multiplier = 2;
                break;
        }
        growthRate = sun.GetComponent<Light2D>().pointLightInnerRadius * multiplier * (float).000001/Time.deltaTime ; 
    }

    
}
