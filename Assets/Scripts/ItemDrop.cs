using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    List<Drop> drops = new List<Drop>();
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.GetComponent<Properties>() != null && this.gameObject.GetComponent<Properties>().objectId != null)
        {
            SetDropListByObjectId(this.gameObject.GetComponent<Properties>().objectId);
        }
        else
        {
            SetDropsList();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (drops.Count <= 0)
        {
            if (this.gameObject.GetComponent<Properties>() != null && this.gameObject.GetComponent<Properties>().objectId != null)
            {
                SetDropListByObjectId(this.gameObject.GetComponent<Properties>().objectId);
            }
            else
            {
                SetDropsList();
            }
        }
    }

    public void OnDestroy()
    {
        CreateObjectFromDropList();
    }

    private void SetDropsList()
    {
        string tag = this.gameObject.name;
        switch(tag)
        {
            case "Tree":
                drops.Add(new Drop(100, 5, 1));
                break;
            case "CopperNode":
                drops.Add(new Drop(75, 3, 3));
                drops.Add(new Drop(75, 2, 3));
                break;
            case "IronNode":
                drops.Add(new Drop(75, 3, 3));
                drops.Add(new Drop(75, 2, 4));
                break;
        }
    }

    private void SetDropListByObjectId(int objectId)
    {
        switch(objectId)
        {
            case 8:
                drops.Add(new Drop(75, 3, 3));
                drops.Add(new Drop(75, 2, 9));
                break;
            case 10:
                drops.Add(new Drop(100, 5, 1));
                break;
        }
    }
    
    private void CreateObjectFromDropList()
    {

        List<Vector3> possitionsOfCreatedObjects = new List<Vector3>();
        foreach(Drop drop in drops)
        {
            for(int i =0; i<drop.NumberOfDrop;i++)
            {
                if (CaculateOddsOfDrop(drop.DropRate))
                {
                    bool postionSet = false;
                    Vector3 vector3 = new Vector3();
                    while (!postionSet)
                    {
                        vector3 = new Vector3(this.gameObject.transform.position.x + (1 * Random.Range(-3, 3)), this.gameObject.transform.position.y + (1 * Random.Range(-3, 3)));
                        if (vector3 != this.gameObject.transform.position && possitionsOfCreatedObjects.Find(p => p == vector3) == new Vector3(0, 0, 0))
                        {
                            possitionsOfCreatedObjects.Add(vector3);
                            postionSet = true;
                        }
                    }
                    this.gameObject.GetComponent<ObjectCreator>().createObject(drop.ItemId, vector3);
                }

            }

        }

    }
    

    private bool CaculateOddsOfDrop(int prob)
    {
        int chance = Random.Range(0, 100);
        if (chance <= prob)
        {
            return true;
        }

        return false;
    }

}
