using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    bool facingRight = true;
    bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 1f;
    public LayerMask whatIsGround;
    private Rigidbody2D rb2D;
    private SpriteRenderer sprite;
    private Animator animator;

    [SerializeField]
    private int score;

    public float move;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        score = 0;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGround();
    }

    void Update()
    {
        State = CharState.Idle;

        
        if (grounded && Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetButton("Horizontal"))
            Run();

        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    Application.Quit();
        //}

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("main");
        }


    }

    private void CheckGround()
    {
        
        grounded = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius, whatIsGround).Length > 0;
        if (!grounded)
            State = CharState.Jump;
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector2.MoveTowards(transform.position, transform.position + direction, maxSpeed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0f;

        State = CharState.Run;
    }

    private void Jump()
    {
        
        rb2D.AddForce(new Vector2(0f, jumpForce));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.tag == "coin")
        {
            score++;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "endLevel") {
            if (!(GameObject.Find("coin"))) SceneManager.LoadScene("1");
        }

        if (collision.gameObject.tag == "Die")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), "Coins: " + score.ToString());
    }

    
}

public enum CharState
{
    Idle,
    Run,
    Jump
}