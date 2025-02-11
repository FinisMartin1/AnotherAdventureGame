using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> invetory = new List<GameObject>();
    public int maxCarryCapacity = 0;
    public int currentCarryCapcity = 0;
    //Todo: maybe do this by id
    public GameObject inHand = null;
    // Start is called before the first frame update
    void Start()
    {
        maxCarryCapacity = (int)this.gameObject.GetComponent<Stats>().Strength * 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
