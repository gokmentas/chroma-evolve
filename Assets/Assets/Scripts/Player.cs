using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float speed = 0.1f;

    [SerializeField]
    private float pushForce = 3f;

    private float movementX;

    private Rigidbody2D rigidbody2;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private string WALK_ANIMATION = "Walk";

    public Text scoreText;

    private float score;

    public Transform cam;

    public GameObject pan;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(rigidbody2.velocity.y > 0 && transform.position.y > score)
        {
            score = transform.position.y;
        }
        scoreText.text = Mathf.Round(score).ToString();

        if(cam.position.y > transform.position.y + 7f)
        {
            Time.timeScale = 0f;
            pan.SetActive(true);
        }

    }

    private void FixedUpdate()
    {
        MoveWithKeyboard();
        PlayerAnimate();
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

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Platform")){
            rigidbody2.AddForce(new Vector2(0f, pushForce), ForceMode2D.Impulse);
        }
    }

    public void GameLoad()
    {
        SceneManager.LoadScene(0);
    }

} // class
