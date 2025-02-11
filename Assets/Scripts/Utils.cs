using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{


    public static List<GameObject> GetAllGameObjects()
    {
        List<GameObject> allActiveObject = new List<GameObject>();
        GameObject[] tempActiveObject = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject gameObject in tempActiveObject)
        {
            allActiveObject.Add(gameObject);
        }
        return allActiveObject;
    }
    public static TextMesh CreateWorldText(string text,Transform parent, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment=TextAlignment.Left, int sortingOrder=5000)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    public static Vector3 GetMouseWorldPostion()
    {
        Vector3 vec = GetMouseWorldPostionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPostionWithZ()
    {
        return GetMouseWorldPostionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPostionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPostionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPostionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    public static GameObject GetGameObjectAtMousePossition()
    {
        Vector2 mousePosstion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        Debug.Log("No Hit");
        if (hit.collider != null)
        {
            Debug.Log("We Got A Hit");
            Debug.Log(hit.collider.gameObject.name);
            return hit.collider.gameObject;
        }
        return null;
    }

    public static bool IsNearPosistion(Vector3 current, Vector3 otherPawn, float range)
    {
        float x = current.x;
        float y = current.y;

        if(x+range> otherPawn.x && x-range < otherPawn.x && y+range> otherPawn.y && y-range< otherPawn.y)
        {
            return true;
        }
        return false;
    }


}

