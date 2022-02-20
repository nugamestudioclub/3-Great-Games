using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEPacingController : PlatformEnemyController
{
    private float xDir = -1f;
    [SerializeField]
    private ColliderController sides;
    [SerializeField]
    private ColliderController head;
    bool switchingDirection = false;
    protected override void Move()
    {
        if (!sides.isColliding)
        {
            switchingDirection = false;
        }
        if (sides.isColliding && !switchingDirection)
        {
            switchingDirection = true;
            xDir *= -1;
        }

        rb.velocity = new Vector2(xDir * moveSpeed, rb.velocity.y);
    }

    protected override void Update()
    {
        base.Update();
        if (head.isColliding && !IsDying)
        {
            Die();
        }
    }
}
