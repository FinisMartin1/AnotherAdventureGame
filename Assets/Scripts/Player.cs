using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveDelta = new Vector3(x, y, 0);

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y *20* Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (moveDelta.y == -1)
        {
            moveDelta.y = -20;
        }
        if (moveDelta.y == 1)
        {
            moveDelta.y = 20;
        }
        if (hit.collider==null )
        {
     

            transform.Translate(0,moveDelta.y * Time.deltaTime,0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x *20* Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            if (moveDelta.x == -1)
            {
                moveDelta.x = -20;
            }
            if (moveDelta.x == 1)
            {
                moveDelta.x = 20;
            }
            transform.Translate( moveDelta.x * Time.deltaTime,0, 0);
        }
    }
}
