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

    private CharacterController controller;
    private Vector2 toMove;

    void Awake() {
        input = GetComponentInChildren<IGlitchyInput>();
        //rb = GetComponentInChildren<Rigidbody2D>();
        controller = GetComponentInChildren<CharacterController>();
        controller.enabled = true;
        feet = GetComponentInChildren<ColliderController>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update() {
        
        Move(input.Movement());
        if (!feet.isColliding)
        {
            //play jump animation
            controller.Move(new Vector2(0, Physics2D.gravity.y) * Time.deltaTime);
            animator.Play("Pplayer_falling");
        }
    }

    private void Move(Vector2 direction) {
        if (controller.isGrounded && toMove.y < 0)
        {
            toMove.y = 0f;
        }
        if ( Mathf.Abs(direction.x) > Mathf.Epsilon )
        {
            if (direction.x > Mathf.Epsilon)
            {//flip to other direction
                sprite.flipX = false;
            } 
            else
            {
                sprite.flipX = true;
            }
            controller.Move(new Vector2(direction.x * moveSpeed, 0) * Time.deltaTime);
            //rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Pplayer_running") && feet.isColliding)
            {
                animator.Play("Pplayer_running");
            }
        } else
        {
            if (feet.isColliding)
            {
                animator.Play("Pplayer_idle");
                //controller.Move(new Vector2(0, controller.velocity.y) * Time.deltaTime);
                //rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        if (input.Jumping())
            Jump();
        toMove.y += Physics2D.gravity.y * Time.deltaTime;
        controller.Move(toMove * Time.deltaTime);
    }

    private void Jump() {
        if (controller.isGrounded/*feet.isColliding*/)
        {
            toMove.y = Mathf.Sqrt(-jumpPower * 5 * Physics2D.gravity.y);
            //controller.Move(new Vector2(controller.velocity.x, jumpPower/4));
            //rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }
}