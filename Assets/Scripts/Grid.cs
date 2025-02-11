using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Grid<TGridObject>
{
    private int width;
    private int height;
    private float cellSize;
    private TGridObject[,] gridArray;
    private TextMesh[,] debugTextArray;
    private Vector3 orginPosition;

    public Grid(int width, int height, float cellSize, Vector3 orginPosition, Func<Grid<TGridObject>,int,int,TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.orginPosition = orginPosition;

   


        gridArray = new TGridObject[width, height];
        debugTextArray = new TextMesh[width, height];
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this,x,y);
            }
        }
                for (int x=0; x < gridArray.GetLength(0); x++)
        {
            for(int y=0; y<gridArray.GetLength(1);y++)
            {

                Debug.DrawLine(GetWorldPostion(x, y), GetWorldPostion(x, y+1), Color.white, Mathf.Infinity);
                Debug.DrawLine(GetWorldPostion(x, y), GetWorldPostion(x+1, y), Color.white, Mathf.Infinity);
            }
        }
        Debug.DrawLine(GetWorldPostion(0, height), GetWorldPostion(width, height), Color.white, Mathf.Infinity);
        Debug.DrawLine(GetWorldPostion(width, 0), GetWorldPostion(width, height), Color.white, Mathf.Infinity);


    }

    
    private Vector3 GetWorldPostion(int x, int y)
    {
        return new Vector3(x, y) * cellSize + orginPosition;
    }

    public void GetXY(Vector3 worldPostion,out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPostion-orginPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPostion - orginPosition).y / cellSize);
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return this.cellSize;
    }
    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
    }

    public void SetGridObjecte(Vector3 worldPostion, TGridObject value)
    {
        int x, y;
        GetXY(worldPostion, out x, out y);
        SetGridObject(x, y, value);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }
    
    public List<TGridObject> GetGridObjectNextTo(int x , int y)
    {
        List<TGridObject > list = new List<TGridObject>();
        if (x-1 >= 0 && y-1 >= 0 && x < width && y < height)
        {
            list.Add(gridArray[x - 1, y - 1]);
        }
        if (x - 1 >= 0 && y + 1 >= 0 && x < width && y < height)
        {
            list.Add(gridArray[x - 1, y + 1]);
        }
        if (x + 1 >= 0 && y - 1 >= 0 && x < width && y < height)
        {
            list.Add(gridArray[x + 1, y - 1]);
        }
        if (x + 1 >= 0 && y + 1 >= 0 && x < width && y < height)
        {
            list.Add(gridArray[x + 1, y + 1]);
        }
        if (x + 1 >= 0 && y >= 0 && x < width && y < height)
        {
            list.Add(gridArray[x + 1, y]);
        }
        if (x - 1 >= 0 && y >= 0 && x < width && y < height)
        {
            list.Add(gridArray[x - 1, y]);
        }
        if (x >= 0 && y + 1 >= 0 && x < width && y < height)
        {
            list.Add(gridArray[x, y + 1]);
        }
        if (x >= 0 && y - 1 >= 0 && x < width && y < height)
        {
            list.Add(gridArray[x, y - 1]);
        }

        return list;
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }

    public TGridObject[,] GetGridArray()
    {
        return gridArray;
    }

    public void SetGridArray(TGridObject[,] gridArray)
    {
        this.gridArray = gridArray;
    }

   
}
