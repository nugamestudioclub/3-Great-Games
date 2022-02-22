using UnityEngine;

[RequireComponent(typeof(IGlitchyInput))]
public class PlatformerPlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    public float jumpPower = 5f;
    public float moveSpeed = 5f;
    public float acceleration = 1;
    public float maxSpeed = 10;

    private ColliderController feet;

    private IGlitchyInput input;

    private SpriteRenderer sprite;
    private Animator animator;


    void Awake() {
        input = GetComponentInChildren<IGlitchyInput>();
        rb = GetComponentInChildren<Rigidbody2D>();
        feet = GetComponentInChildren<ColliderController>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update() {
        if( input.Jumping() )
            Jump();
        Move(input.Movement());
        if (!feet.isColliding)
        {
            //play jump animation
            animator.Play("player_falling");
        }
    }

    private void Move(Vector2 direction) {
        if( Mathf.Abs(direction.x) > Mathf.Epsilon )
        {
            if (direction.x > Mathf.Epsilon)
            {//flip to other direction
                sprite.flipX = false;
            } 
            else
            {
                sprite.flipX = true;
            }
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("player_running") && feet.isColliding)
            {
                animator.Play("player_running");
            }
        } else
        {
            if (feet.isColliding)
            {
                animator.Play("player_idle");
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    private void Jump() {
        if (feet.isColliding)
        {
            
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        

    }
}