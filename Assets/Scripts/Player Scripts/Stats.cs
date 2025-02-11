using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int myTeam = -1;
    public int Level = 1;
    public int Vatality;
    public int Endurance;
    public float Strength;
    public int Dextarity;
    public int Intellagence;
    public int Faith;
    public int Agility;
    public int Perception;
    public int Luck;

    public int health;
    public int attackPower;
    public int numberOfSlots;

    public int plantCutting;
    public int constructing;
    public int mining;
    public float speed;
    // Start is called before the first frame update
    private void Start()
    {
        
        Vatality = 1;
        Endurance = 1;
        Strength = 1;
        Dextarity = 1;
        Intellagence = 1;
        Faith = 1;
        Agility = 10;
        Perception = 1;
        Luck = 1;
        plantCutting = 1 + (int)(Strength * .5);
        constructing = 1;
        GetStatsForObect();

        health = (Vatality * 2);
        numberOfSlots = (int)Mathf.Ceil((float)(0.25 * this.GetComponent<Stats>().Strength));
        speed = Agility * 0.25f;

    }

    // Update is called once per frame
    private void Update()
    {
        if(health<=0)
        {
            Destroy(gameObject);
        }
    }

    private void GetStatsForObect()
    {
        if(this.name=="Tree")
        {
            Vatality = 5;
            Strength = 4;
        }

        if(this.name=="Player")
        {
            plantCutting = 24;
            constructing = 25;
            mining = 24;
        }
    }
}
