using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 7.0f;
    public float jumpForce = 20.0f;
    public float dashForce = 15.0f;

    private Animator _animator;
    private Vector2 _movementVector;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _renderer;

    public int jumpCount;
    public bool isGrounded = true;

    private float dashDelay;
    public float dashRate;
    
    [SerializeField] private AudioSource jumpSoundEffect;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>(); // Get the Rigidbody from the player GameObject
        _animator = GetComponent<Animator>(); // Get the Animator from the player GameObject
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
            _animator.SetBool("isRunning", true);
            _renderer.flipX = true;
        }
        else if (_movementVector.x > 0)
        {
            _animator.SetBool("isRunning", true);
            _renderer.flipX = false;
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
        
        dashDelay -= Time.deltaTime;
    }

    private void Jump()
    {
        if (jumpCount < 2)
        {
            isGrounded = false;
            jumpCount++;
            jumpSoundEffect.Play();
            _rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            _animator.SetBool("isJumping", true);
            _animator.SetBool("isGrounded", false);
        }
    }

    private void Dash()
    {
        if (dashDelay < 0)
        {
            dashDelay = dashRate;
            _rigidBody.AddForce(new Vector2(dashForce, 0), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _animator.SetBool("isGrounded", true);
            _animator.SetBool("isJumping", false);
            isGrounded = true;
            jumpCount = 0;
        }
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Game Over!");
            Destroy(this.gameObject);
            SceneManager.LoadScene("EndMenu");
        }
        
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Level passed!");
            SceneManager.LoadScene("EndMenu");
        }
    }
}
