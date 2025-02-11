using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilityList : MonoBehaviour
{

    public int damage;
    public string name;
    public int moveCost=0;
    public bool inUse = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (name != null)
        {
            UseAbility();
        }
    }

    public void UseAbility()
    {
        if(name=="Basic Attack")
        {
            damage = this.GetComponent<Stats>().attackPower;
            moveCost = 1;
        }
    }
}
