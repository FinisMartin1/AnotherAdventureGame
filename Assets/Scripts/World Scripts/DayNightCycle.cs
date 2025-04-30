using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    public float maxTime = 1440f;
    public float currentTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = currentTime + Time.deltaTime;
        if(currentTime>=360)
        {
            StartSunrise();
        }
        if(currentTime>=420)
        {
            MoveSun();
        }
        if(currentTime>= 1000)
        {
            StartSunset();
        }
    }
    
    void StartSunset()
    {
        if(this.GetComponent<Light2D>().pointLightInnerRadius>0)
        {
            this.GetComponent<Light2D>().pointLightInnerRadius = this.GetComponent<Light2D>().pointLightInnerRadius - Time.deltaTime;
        }
    }
    void StartSunrise()
    {
        if(this.GetComponent<Light2D>().pointLightInnerRadius < 60)
        {
            this.GetComponent<Light2D>().pointLightInnerRadius = this.GetComponent<Light2D>().pointLightInnerRadius + Time.deltaTime;
        }
    }

    void MoveSun()
    {

        if(this.transform.position.x < 140)
        {
            Vector3 position = this.transform.position;
            position.x = position.x + Time.deltaTime/6;
            this.transform.position = position;
        }
    }
}
