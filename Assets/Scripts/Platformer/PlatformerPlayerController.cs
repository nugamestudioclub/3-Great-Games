using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpPower = 5f;
    public float moveSpeed = 5f;
    public float acceleration = 1;
    public float maxSpeed = 10;
    
    private ColliderController feet;
    void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        feet = GetComponentInChildren<ColliderController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }

    void GetInputs()
    {
        float xDir = Input.GetAxis("Horizontal");
        if (feet.isColliding && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (/*Mathf.Abs(rb.velocity.x) < maxSpeed &&*/ Mathf.Abs(xDir) > Mathf.Epsilon)
        {
            Move(xDir);
        }
    }
    void Jump()
    {
        rb.velocity = new  Vector2 (rb.velocity.x, jumpPower);

    }

    private void Move(float xDir)
    {
        //rb.
        rb.velocity = new Vector2(xDir * moveSpeed, rb.velocity.y);
        //rb.AddForce(Vector2.right * acceleration * moveSpeed * Time.deltaTime * 10);
    }


}
