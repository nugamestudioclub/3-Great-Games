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

    void Awake() {
        input = GetComponentInChildren<IGlitchyInput>();
        rb = GetComponentInChildren<Rigidbody2D>();
        feet = GetComponentInChildren<ColliderController>();
    }

    private void Update() {
        if( input.Jumping() )
            Jump();
        Move(input.Movement());
    }

    private void Move(Vector2 direction) {
        if( Mathf.Abs(direction.x) > Mathf.Epsilon )
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        //rb.AddForce(Vector2.right * acceleration * moveSpeed * Time.deltaTime * 10);
    }

    private void Jump() {
        if( feet.isColliding )
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
    }
}