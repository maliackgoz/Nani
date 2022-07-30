using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 7.0f;
    public float jumpForce = 20.0f;

    //private Animator _animator;
    private Vector2 _movementVector;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _renderer;

    public int jumpCount;
    public bool isGrounded = true;
    
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>(); // Get the Rigidbody from the player GameObject
        //_animator = GetComponent<Animator>(); // Get the Animator from the player GameObject
        _renderer = GetComponent<SpriteRenderer>(); // Get the Sprite Renderer from the player GameObject
        
        jumpCount = 0;
    }

    
    void Update()
    {
        _movementVector = new Vector2(Input.GetAxis("Horizontal"), 0);
        if (isGrounded == false)
        {
            playerSpeed = 6.0f;
        }
        else
        {
            playerSpeed = 7.0f;
        }
        transform.Translate(Time.deltaTime * playerSpeed * _movementVector); // Horizontal Movement
        if (_movementVector.x < 0)
        {
            _renderer.flipX = true;
        }
        else if (_movementVector.x > 0)
        {
            _renderer.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    private void Jump()
    {
        if (jumpCount < 2)
        {
            isGrounded = false;
            jumpCount++;
            _rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
}
