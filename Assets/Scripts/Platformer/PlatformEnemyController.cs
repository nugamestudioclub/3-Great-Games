using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public abstract class PlatformEnemyController : MonoBehaviour
{
    public bool IsDying { get; private set; }
    public float moveSpeed;
    protected Rigidbody2D rb;
    private Animator animator;
    protected SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
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
        animator.Play("ladybug_death");

        IsDying = true;
        Debug.Log($"{gameObject.name} has died!");
    }

    abstract protected void Move();
}
