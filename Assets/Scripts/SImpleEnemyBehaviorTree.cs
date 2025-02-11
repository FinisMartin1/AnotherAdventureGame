using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyBehaviorTree : MonoBehaviour
{
    public GameObject target;
    public GameObject _self;
    public bool isNextToTarget = false;
    
    private void Update()
    {
        
   
    }

    private void CheckIfNextToTarget(Vector3 position,Vector3 scale, int moves)
    {
        List<Vector3> allPossibleMoves = new List<Vector3>();
        int row = 0;
        for (int y = 0; y < moves+1; y++)
        {
            for (int x = 0+row; x < (moves * 2)+1-row; x++)
            {
                allPossibleMoves.Add(new Vector3(position.x + (scale.x * x), position.y + (scale.y * y)));
            }
            row++;
        }
        row = 1;
        for (int y = 1; y < moves + 1; y++)
        {
            for (int x = 0 + row; x < (moves * 2) + 1 - row; x++)
            {
                allPossibleMoves.Add(new Vector3(position.x + (scale.x * x), position.y - (scale.y * y)));
            }
            row++;
        }

        allPossibleMoves = GetWalkableList(allPossibleMoves);
        Vector3 move=GetShortestDistanceNode(allPossibleMoves);


        if (Pathfinding.Instance.GetGrid().GetGridObject(move) != Pathfinding.Instance.GetGrid().GetGridObject(this.transform.position))
        {
            isNextToTarget = false;
            _self.GetComponent<NPCPathfinding>().SetPathToPlayer(move);
        }
        else
        {
            isNextToTarget = true;
        }
        
       
    }

    private List<Vector3> GetWalkableList(List<Vector3> positions)
    {

        List<Vector3> newPositionList = new List<Vector3>();
        foreach(Vector3 position in positions)
        {
            if (Pathfinding.Instance.GetGrid().GetGridObject(position) != null)
            {


                if (Pathfinding.Instance.GetGrid().GetGridObject(position).isWalkable==true)
                {
                    newPositionList.Add(position);
                }
            }
        }
        return newPositionList;
    }

    private Vector3 GetShortestDistanceNode(List<Vector3> positions)
    {
        Vector3 shortestDistance = positions[0];
        float smallestDistance= Vector3.Distance(positions[0], target.transform.position);

        foreach (Vector3 position in positions)
        {
            float distance = Vector3.Distance(position, target.transform.position);
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                shortestDistance = position;
            }

        }

        return shortestDistance;
    }
}
