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

    public Text scoreText;

    private GameObject lastPlatform;

    public Text redText, yellowText;

    private float score;
    private float redScore, yellowScore = 0;

    public Transform cam;

    public GameObject pan, boost, eatedApple, eatedBanana;

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
            rigidbody2.AddForce(new Vector2(0f, pushForce), ForceMode2D.Impulse);
        }

        if(other.gameObject.name.Contains("Platform_Red") && other.gameObject != lastPlatform){
            redScore += 1;
            redText.text = redScore.ToString();
            lastPlatform = other.gameObject;
        }else if(other.gameObject.name.Contains("Platform_Yellow") && other.gameObject != lastPlatform){
            yellowScore += 1;
            yellowText.text = yellowScore.ToString();
            lastPlatform = other.gameObject;
        }
    }

    public void GameLoad()
    {
        SceneManager.LoadScene(0);
    }

    public void RedGmeLoad(){
        if(redScore >= 1){
        eatedApple.SetActive(true);
        pan.SetActive(false);
        Time.timeScale = 1f;
        transform.position = lastPlatform.transform.position;
        speed = 6;
        rigidbody2.gravityScale = 2f;
        }
    }

    public void YellowGmeLoad(){
        if(yellowScore >= 1){
        eatedBanana.SetActive(true);
        pan.SetActive(false);
        Time.timeScale = 1f;
        transform.position = lastPlatform.transform.position;
        rigidbody2.gravityScale = 1.6f;
        speed = 3;
        }
    }

} // class
