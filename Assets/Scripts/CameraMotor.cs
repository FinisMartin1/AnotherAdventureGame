using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;
        if(deltaX>boundX || deltaX < -boundX)
        {
            if(transform.position.x<lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, deltaY, 0);

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            this.GetComponent<Camera>().orthographicSize--;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            this.GetComponent<Camera>().orthographicSize++;
        }
    }
}
