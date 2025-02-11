using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSelector : MonoBehaviour
{
    public GameObject select;
    public SelectObject.ActionSelector action;
    public SelectObject.Selectors selector;
    public string meterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        select.GetComponent<SelectObject>().SetActionSelector(action);
        select.GetComponent<SelectObject>().SetSelector(selector);
        if(meterial!= null)
        {
            select.GetComponent<SelectObject>().meterial = meterial;
        }
    }
}
