using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

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

    public Text scoreText, limitRedText, LimitYellowText;

    private int limit;

    private GameObject lastPlatform;

    public Text redText, yellowText;

    private float score;
    private float redScore, yellowScore = 0;

    public Transform cam;

    public GameObject pan, boost, eatedApple, eatedBanana;

    public AudioSource jump, collect, respawn;

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
        limit = 1;
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
            if(boost != null){
                boost.SetActive(true);
            }
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
            rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, 0f);
            rigidbody2.AddForce(new Vector2(0f, pushForce), ForceMode2D.Impulse);
            jump.Play();
            lastPlatform = other.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Fruit")){
            collect.Play();
            if(other.gameObject.name.Contains("Apple"))
            {
                redScore += 1;
                redText.text = redScore.ToString();
            }else if(other.gameObject.name.Contains("Banana"))
            {
                yellowScore += 1;
                yellowText.text = yellowScore.ToString();
            }            
            Destroy(other.gameObject);
        }
    }

    public void GameLoad()
    {
        SceneManager.LoadScene(0);
    }

    public void RedGmeLoad()
    {
        if (redScore >= limit)
        {
            respawn.Play();
            eatedApple.SetActive(true);
            eatedBanana.SetActive(false);
            pan.SetActive(false);
            redScore = 0;
            yellowScore = 0;
            redText.text = "0";
            yellowText.text = "0";
            Time.timeScale = 1f;
            transform.position = lastPlatform.transform.position;
            speed = 6;
            rigidbody2.gravityScale = 2f;
            limit *= 2;
            LimitYellowText.text = limit.ToString();
            limitRedText.text = limit.ToString();
        }
    }

    public void YellowGmeLoad()
    {
        if (yellowScore >= limit)
        {
            respawn.Play();
            eatedBanana.SetActive(true);
            eatedApple.SetActive(false);
            pan.SetActive(false);
            redScore = 0;
            yellowScore = 0;
            redText.text = "0";
            yellowText.text = "0";
            Time.timeScale = 1f;
            transform.position = lastPlatform.transform.position;
            rigidbody2.gravityScale = 1.6f;
            speed = 3;
            limit *= 2;
            LimitYellowText.text = limit.ToString();
            limitRedText.text = limit.ToString();
        }
    }

} // class
