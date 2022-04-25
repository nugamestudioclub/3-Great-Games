using UnityEngine;

[RequireComponent(typeof(IGlitchyInput))]
public class PlatformerPlayerController : MonoBehaviour {
   
    public float jumpPower = 5f;
    public float moveSpeed = 5f;
    public float acceleration = 1;
    public float maxSpeed = 10;

    private IGlitchyInput input;
    private Rigidbody2D rb;
    private ColliderController feet;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AudioSource jumpSound;

    void Awake() {
        input = GetComponentInChildren<IGlitchyInput>();
        rb = GetComponentInChildren<Rigidbody2D>();
        feet = GetComponentInChildren<ColliderController>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        jumpSound = GetComponent<AudioSource>();
    }

    private void Update() {
        if( input.Jumping() )
            Jump();
        Move(input.Movement());
        if (!feet.isColliding)
        {
            //play jump animation
            animator.Play("Pplayer_falling");
        }
    }

    private void Move(Vector2 direction) {
        if( Mathf.Abs(direction.x) > Mathf.Epsilon )
        {
            if (direction.x > Mathf.Epsilon)
            {//flip to other direction
                spriteRenderer.flipX = false;
            } 
            else
            {
                spriteRenderer.flipX = true;
            }
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Pplayer_running") && feet.isColliding)
            {
                animator.Play("Pplayer_running");
            }
        } else
        {
            if (feet.isColliding)
            {
                animator.Play("Pplayer_idle");
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    private void Jump() {
        if (feet.isColliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpSound.Play();
        }
        

    }
}