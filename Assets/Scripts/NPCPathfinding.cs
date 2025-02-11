using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPathfinding : MonoBehaviour
{
    private const float speed = 40f;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    private int maxMoves = 5;
    public int moves = 5;

    private void Update()
    {
    
        HandleMovement();

    }
    private void HandleMovement()
    {
        if (pathVectorList != null && (pathVectorList.Count - 1) < moves)
        {
            Vector3 targetPostion = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPostion) > 1f)
            {
                Vector3 moveDir = (targetPostion - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPostion);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();

                }
            }
        }
        else
        {
 
        }
    }

        private void StopMoving()
    {
        moves = moves - pathVectorList.Count;
        pathVectorList = null;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public List<Vector3> GetPath()
    {
        return pathVectorList;
    }
    public void SetPathToPlayer(Vector3 end)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), end);
        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }
    public void ResetMoeves()
    {
        moves = maxMoves;
    }
}
