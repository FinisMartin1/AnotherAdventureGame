using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpace 
{


    private Grid<PathNode> grid;
    public AttackSpace(int width, int height, Vector3 orgin)
    {

        Vector3 newPostion = new Vector3(orgin.x - (10 * width)/2, orgin.y - (10 * height)/2);
        grid = new Grid<PathNode>(width, height, 10f, newPostion, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
