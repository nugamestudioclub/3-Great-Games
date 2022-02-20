using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public abstract class PlatformEnemyController : MonoBehaviour
{
    public bool IsDying { get; private set; }
    public float moveSpeed = 5f;
    protected Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!IsDying)
        {
            //do normal behavior
            Move();
        } 
    }

    protected void Die()
    {
        //play sound
        //play animation

        IsDying = true;
        Debug.Log($"{gameObject.name} has died!");
    }

    abstract protected void Move();
}
