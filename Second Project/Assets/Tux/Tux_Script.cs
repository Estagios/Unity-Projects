using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tux_Script : MonoBehaviour
{
    public List<string> items;

    Vector3 _initialPosition;


    Rigidbody2D rb2d;
    [SerializeField]
    private float moveSpeed = 4;

    public GameObject scoreUI;

    public const string RIGHT = "right";
    public const string LEFT = "left";

    bool isgrouded;

    public int score = 0;

    [SerializeField]
    private float knockbackstrength;

    [SerializeField]
    public float knockback;
    [SerializeField]
    public float knockbacklength;
    [SerializeField]
    public float knockbackcount;

    public bool knockbackright;



    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    Transform groundCheckL;
    [SerializeField]
    Transform groundCheckR;

    Animator animator;
    SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        items = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreUI.gameObject.GetComponent<Text>().text = ("X " + score);

    }


    private void FixedUpdate()
    {
        if (transform.position.y < -9)
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if ((Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
           (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) ||
           (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))) ||
           (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Enimie"))) ||
           (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Enimie"))) ||
           (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Enimie"))) ||
           (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Wall"))) ||
           (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Wall"))) ||
           (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Wall"))))
        {
            isgrouded = true;
        }
        else
        {
            isgrouded = false;
            animator.Play("Jump");
        }

        if (knockbackcount <= 0)
        {
            if (Input.GetKey("d") || Input.GetKey("right"))
            {
                rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);

                if (isgrouded)
                    animator.Play("Walk");
                sprite.flipX = false;
            }
            else if (Input.GetKey("a") || Input.GetKey("left"))
            {
                rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);

                if (isgrouded)
                    animator.Play("Walk");
                sprite.flipX = true;
            }
            else
            {
                if (isgrouded)
                    animator.Play("Idle");
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }

            if (Input.GetKey("space") && isgrouded == true)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 10);
                animator.Play("Jump");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            string itemType = collision.gameObject.GetComponent<CollectablleScript>().itemType;
            print("Coin");
            score += 1;
            items.Add(itemType);

            Destroy(collision.gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool hit = collision.collider.GetComponent<Enemies_AI>() != null;
        bool head = collision.collider.GetComponent<HeadDetectScript>() != null;

        if (hit)
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (head)
            Destroy(collision.gameObject);
    }   
}