using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    enum PerformAction{
        noAction = 0,
        chopTree = 1 
    };
    PerformAction selectedAction = PerformAction.noAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("c"))
        {
            selectedAction = PerformAction.chopTree;
        }
    }
}
