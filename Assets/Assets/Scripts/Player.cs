using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float jumpForce = 11f;

    private float movementX;

    private Rigidbody2D rigidbody2;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private string WALK_ANIMATION = "Walk";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithKeyboard();
        PlayerAnimate();
        PlayerJump();
    }

    private void FixedUpdate()
    {

    }

    void MoveWithKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        transform.position += new Vector3(movementX, 0, 0);
    }

    void PlayerAnimate()
    {
        if(movementX > 0)
        {
            animator.SetBool(WALK_ANIMATION, true);
            spriteRenderer.flipX = false;
        }
        else if(movementX < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool(WALK_ANIMATION, true);
        }
        else
        {
            animator.SetBool(WALK_ANIMATION, false);
        }
    }

    void PlayerJump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            rigidbody2.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

    }


} // class
