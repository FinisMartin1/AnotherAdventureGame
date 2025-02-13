using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBuildObjectSelector : MonoBehaviour
{

    public GameObject select;
    public GameObject buildPanel;
    public int ObjectId = 0;
    
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
        select.GetComponent<SelectObject>().buildObjectId = ObjectId;
        select.GetComponent<SelectObject>().actionSelector = SelectObject.ActionSelector.BuildObjectBed;
        buildPanel.SetActive(false);
        select.SetActive(true);

    }
}
