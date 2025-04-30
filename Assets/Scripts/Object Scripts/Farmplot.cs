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
    public SeedType seed = SeedType.NONE;
    private GameObject sun = null;
    private float maturity = 0;
    private float growthRate = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sun == null)
        {
            sun = Utils.GetAllGameObjects().First(g => g.GetComponent<DayNightCycle>() != null);
        }
        
        if(seed != SeedType.NONE && sun != null)
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
