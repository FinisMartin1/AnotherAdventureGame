using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Collidable
{
    protected override void onCollide(Collider2D collider)
    {
        Debug.Log("Item Collected");
    }
}
