using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WorldCreator : MonoBehaviour
{

    public int maxWorldWidth = 140;
    public int maxWorldHeight = 140;
    public int maxTrees = 400;
    public int maxIronNodes = 30;
    public int maxCooperNodes = 30;
    public GameObject objectCreator;
    public bool worldCreated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!worldCreated)
        {
            CreateWorld();
            worldCreated = true;
        }
    }
    
    private void CreateWorld()
    {
        float x = 0;
        float y = 0;
        //populate tree
        for(int i = 0; i < maxTrees; i++)
        {
            x = UnityEngine.Random.Range(0, maxWorldWidth);
            y = UnityEngine.Random.Range(0, maxWorldHeight);
            objectCreator.GetComponent<ObjectCreator>().createObject(10, new Vector3(x, y));
        }

        for(int i = 0; i < maxIronNodes; i++)
        {
            x = UnityEngine.Random.Range(0, maxWorldWidth);
            y = UnityEngine.Random.Range(0, maxWorldHeight);
            objectCreator.GetComponent<ObjectCreator>().createObject(8, new Vector3(x, y));
        }

        int numberOfWaterSources = UnityEngine.Random.Range(1, 8);

        for(int i = 0; i < numberOfWaterSources; i++)
        {
            CreateLake(UnityEngine.Random.Range(5, 30), UnityEngine.Random.Range(5, 30), new Vector3(UnityEngine.Random.Range(0, maxWorldWidth), UnityEngine.Random.Range(0, maxWorldHeight)));
        }
    }

    private void CreateLake(int height, int diameter, Vector3 position)
    {
        for (int j = 0; j < height; j++)
        {
            for (int i = j; i < diameter - j; i++)
            {
                Vector3 tempPos = position;
                tempPos.x = position.x + ((float)0.64 * (float)i);
                tempPos.y = position.y + ((float)0.64 * (float)j);
                objectCreator.GetComponent<ObjectCreator>().createObject(11, tempPos);
                tempPos.y = position.y - ((float)0.64 * (float)j);
                objectCreator.GetComponent<ObjectCreator>().createObject(11, tempPos);
            }
        }
        
    }
    
}
