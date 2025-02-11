using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathfinding : MonoBehaviour
{
    private float speed = 10f;
    private int currentPathIndex;
    public List<Vector3> pathVectorList;

    private void Start()
    {

    }
    private void Update()
    {
        speed = this.GetComponent<Stats>().speed;
        HandleMovement();
        

        if (Input.GetMouseButtonDown(0))
        {
            //SetTargetPosition(Utils.GetMouseWorldPostion());
        }
    }
    private void HandleMovement()
    {

        if (pathVectorList != null && pathVectorList.Count>0)
        {

            Vector3 targetPostion = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPostion) > 0.05f)
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
        } else
        {

        }
 
    }

    private void StopMoving()
    {
        pathVectorList = null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            
            pathVectorList.RemoveAt(0);
        }
    }

    public void SetTargetPostionToObject(GameObject gameObject)
    {
        currentPathIndex = 0;
        Debug.Log(gameObject.gameObject.name);
        Vector3 targetPosistion = gameObject.transform.position;
 
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosistion);
        if(pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }

    public void SetTargetPostionNexToPostion(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPathNextTo(this.GetPosition(), targetPosition);

        if(pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }
}

