using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public static Pathfinding Instance { get; private set; }

    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    public Pathfinding(int width, int height)
    {
        Instance = this;
        grid = new Grid<PathNode>(width, height, 0.7f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            if (go.layer == 6 )
            {
                SetUnWalkableNode(go);
            }

        }
    }

    public void SetUnWalkableNode(GameObject o)
    {
        List<Vector3> positions = new List<Vector3>();
        float cellSize = grid.GetCellSize();
        Vector3 topPosition = new Vector3(o.transform.position.x + o.transform.localScale.x / 2, o.transform.position.y + o.transform.localScale.y / 2);
        Vector3 bottomPosition= new Vector3(o.transform.position.x - o.transform.localScale.x / 2, (o.transform.position.y - o.transform.localScale.y / 2));
        for (int y = (int)bottomPosition.y; y < (int)topPosition.y; y++)
        {
            for(int x = (int)bottomPosition.x; x < (int)topPosition.x; x++)
            {
                Vector3 position = new Vector3(x, y);
                if(grid.GetGridObject(position)!= null)
                {
                    grid.GetGridObject(position).isWalkable = false;
                }
            }
        }
    }

    public List<Vector3> FindPath(Vector3 startWorldPosition,Vector3 endWorldPosition)
    {
        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int endY);

        List<PathNode> path = FindPath(startX, startY, endX, endY);

        if (path==null)
        {
            return null;
        } else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach(PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f);
            }
            return vectorPath;
        }
    }

    public List<Vector3> FindPathNextTo(Vector3 startWorldPosition, Vector3 endWorldPosition)
    {
        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int tempEndX, out int tempEndY);
        List<Vector3> availableNodes = this.GetPostionNextToPosition(endWorldPosition);
        float distance = -1;
        Vector3 closestPostion = endWorldPosition;
        foreach (Vector3 position in availableNodes)
        {

            if (distance != -1)
            {
                
                if (distance < Vector3.Distance(startWorldPosition, position))
                {
                    distance = Vector3.Distance(startWorldPosition, position);
                    closestPostion = position;
                }
            }
            else
            {
                distance = Vector3.Distance(startWorldPosition, position);
                closestPostion = position;
            }
        }
        grid.GetXY(closestPostion, out int endX, out int endY);
        List<PathNode> path = FindPath(startX, startY, endX, endY);
        if (path == null)
        {
            Debug.Log("path is null");
            return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f);
            }
            return vectorPath;
        }

    }
    public List<PathNode> FindPath(int startX,int startY,int endX, int endY)
    {
        PathNode startNode = this.grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);
        if (startNode == null || endNode == null)
        {
            return null;
        }
        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for(int x=0;x<grid.GetWidth();x++)
        {
            for(int y=0;y<grid.GetHeight();y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CaculateFCost();
                pathNode.cameFromNode = null;

            }
        }
        startNode.gCost = 0;
        startNode.hCost = CaculateDistanceCost(startNode, endNode);
        startNode.CaculateFCost();
        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                return CalcualatedPath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);
            foreach(PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CaculateDistanceCost(currentNode, neighbourNode);
                if(tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CaculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CaculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        return null;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();
        if (currentNode.x - 1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if (currentNode.x + 1 < grid.GetWidth())
        {
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));
        return neighbourList;
    }

    private PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }
    private List<PathNode> CalcualatedPath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }
    private int CaculateDistanceCost(PathNode a,PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for(int i =1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

    private List<Vector3> GetPostionNextToPosition(Vector3 position)
    {
        List<Vector3> positions = new List<Vector3>();
        positions.Add(new Vector3(position.x + 1, position.y));
        positions.Add(new Vector3(position.x - 1, position.y));
        positions.Add(new Vector3(position.x, position.y + 1));
        positions.Add(new Vector3(position.x, position.y - 1));
        return positions;
        return positions;
    }

    public Grid<PathNode> GetGrid()
    {
        return grid;
    }
}
