using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class ColliderController : MonoBehaviour
{
    [HideInInspector]
    public bool isColliding = false;

    [SerializeField]
    private string myTag;
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (string.IsNullOrEmpty(myTag))
        {
            isColliding = true;
        } else
        {
            if (myTag == collision.tag)
            {
                isColliding = true;
            }
        }
        
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (string.IsNullOrEmpty(myTag))
        {
            isColliding = false;
        }
        else
        {
            if (myTag == collision.tag)
            {
                isColliding = false;
            }
        }
        
    }
}
