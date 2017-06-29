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
    Rigidbody2D rb2D;

    [SerializeField]
    private int score;

    public float move;

    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        score = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        

        move = Input.GetAxis("Horizontal");

    }

    void Update()
    {
        if (grounded && (Input.GetKeyDown("space")))
        {
            rb2D.AddForce(new Vector2(0f, jumpForce));
        }
        rb2D.velocity = new Vector2(move * maxSpeed, rb2D.velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();



        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("main");
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.name == "coin")
        {
            score++;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.name == "endLevel") {
            if (!(GameObject.Find("coin"))) SceneManager.LoadScene("1");
        }

        if (collision.gameObject.name == "dieCollider" || collision.gameObject.name == "saw")
        {
            if (SceneManager.GetActiveScene().name == "1")
                    SceneManager.LoadScene("1");
            else
                SceneManager.LoadScene("main");
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), "Coins: " + score.ToString());
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}