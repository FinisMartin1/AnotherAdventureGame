using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{

    public GameObject woodenLogPrefab;
    public GameObject wallPrefab;
    public GameObject stonePrefab;
    public GameObject copperPrefab;
    public GameObject bedPrefab;
    public GameObject ironPrefab;
    public GameObject showerPrefab;
    public GameObject toiletPrefab;
    public GameObject treePrefab;
    public GameObject ironNode;
    public GameObject waterTile;
    public GameObject wheatPrefab;
    public GameObject wheatSeedPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createObject(int id , Vector3 position)
    {
        GameObject gameObject = new GameObject();
        switch (id)
        {
            case 1://wood logs
                gameObject = Instantiate(woodenLogPrefab);

                break;
            case 2://Wooden wall
                gameObject.AddComponent<SpriteRenderer>();
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player_Box");
                gameObject.AddComponent<BoxCollider2D>();
                gameObject.layer = 6;
                gameObject.name = "Wooden Wall";
                gameObject.transform.position = position;
                break;
            case 3: //stone
                gameObject = Instantiate(stonePrefab);
                break;
            case 4: //copper
                gameObject = Instantiate(copperPrefab);
                break;
            case 5: //bed
                gameObject = Instantiate(bedPrefab);
                break;
            case 6: //shower
                gameObject = Instantiate(showerPrefab);
                break;
            case 7: //toliet
                gameObject = Instantiate(toiletPrefab);
                break;
            case 8:
                gameObject = Instantiate(ironNode);
                break;
            case 9://iron
                gameObject = Instantiate(ironPrefab);
                break;
            case 10://tree
                gameObject = Instantiate(treePrefab);
                break;
            case 11:
                gameObject = Instantiate(waterTile);
                break;
            case 12:
                gameObject = Instantiate(wheatSeedPrefab);
                break;
            case 13:
                gameObject = Instantiate(wheatPrefab);
                break;
        }
        gameObject.transform.position = position;

    }

    public void createWall(Vector3 position)
    {
        GameObject gameObject = new GameObject();
        gameObject = Instantiate(wallPrefab);
        wallPrefab.transform.position = position;
    }
}
