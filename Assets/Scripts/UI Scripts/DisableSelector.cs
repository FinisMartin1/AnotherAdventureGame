using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSelector : MonoBehaviour
{
    public GameObject Selctor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHover()
    {
        Selctor.SetActive(false);
    }

    public void offHover()
    {
        Selctor.SetActive(true);
    }
}
