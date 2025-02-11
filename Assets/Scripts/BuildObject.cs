using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildObject : MonoBehaviour
{

    public enum BuildType
    {
        BuildWall = 0,
        BuildObject = 1

    }

   public enum ObjectType
    {
        Bed,
    }

    public string meterial = "";

    public List<int> recipe = new List<int>();
    public List<int> contains = new List<int>();

    public BuildType buildType = BuildType.BuildWall;
    public bool isBuilding = false;
    public bool isReadyToBuild = false;
    public int objectId = 0;
    public GameObject builder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuilding)
        {

        }
        if(recipe.Count == 0)
        {
            SetRecipe(objectId);
        }
        if(recipe.Count == contains.Count)
        {
            isReadyToBuild = true;
        }
    }

    private void buildWall()
    {
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<SpriteRenderer>();
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player_Box");
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.layer = 6;
        gameObject.name = "Wooden Wall";
        gameObject.transform.position = this.transform.position;
    }

    private void SetRecipe(int objectId)
    {
        switch(objectId)
        {
            case 5:
                recipe.Add(1);
                break;
            case 6:
                recipe.Add(4);
                recipe.Add(4);
                break;
            case 7:
                recipe.Add(4);
                break;
        }
    }
}
