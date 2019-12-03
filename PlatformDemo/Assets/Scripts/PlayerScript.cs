using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //COMPONENTS
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public Animator animator;

    //POSITION
    private Vector2 _startPosition;

    //MOVE
    [SerializeField]
    private float speed = 5f;
    private Vector3 movement;

    //JUMP
    [SerializeField]
    private float jumpHeigth = 5f;
    [SerializeField]
    private bool isGrounded;
    private int extraJumps;

    public int extraJumpValue;

    //UI
    private int _lives = 3;
    private int _pineappleCount;
    public Text lives;
    public Text pineappleCount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        float horInput = Input.GetAxis("Horizontal") * speed;
        if (horInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        
        movement = new Vector3(horInput, 0, 0);
        animator.SetFloat("Speed", Mathf.Abs(horInput));
        transform.position += movement * Time.deltaTime;
        Jump();
    }

    void Jump()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }
        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpHeigth;
            extraJumps--;
        }
        else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpHeigth;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("Jumping", false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = false;
            animator.SetBool("Jumping", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pickup")
        {
            Destroy(collision.gameObject);
            _pineappleCount++;
            pineappleCount.text = _pineappleCount.ToString();
        }
        else if (collision.tag == "DeathTrigger")
        {
            transform.position = _startPosition;
            if (_lives > 1)
            {
                _lives--;
                lives.text = _lives.ToString();
            }
            else
            {
                SceneManager.LoadScene(0);
            }
            
        }
        else if (collision.tag == "Finish")
        {
            //Scene currentScene = SceneManager.GetActiveScene();
            //SceneManager.LoadScene(currentScene.name);
            SceneManager.LoadScene(0);
        }
        
    }
}
