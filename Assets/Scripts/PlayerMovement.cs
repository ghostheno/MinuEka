using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask groundLayer;

    public float speed = 5f;
    public float jumpForce = 5f;
    
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

   
    float direction = 0f;
    bool isFacingRight = true;
    bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        direction = Input.GetAxis("Horizontal");
        MovePlayer(direction);
        SetSpriteDirection();
        
        // Check if the player presses the spacebar and is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }
    void MovePlayer(float direction)
    {
        if (direction != 0f)
        {
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
            isFacingRight = direction > 0f;
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        }
    }
    void SetSpriteDirection()
    {
        spriteRenderer.flipX = !isFacingRight;
    }
    private void Jump()
    {
        // Apply a vertical force to the Rigidbody
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        // Set isGrounded to false to prevent double jumps
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player has landed on the ground
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }


}