using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static bool dead = false;
    public static int score = 0;

    public float speed = 10f;
    public float jumpForce = 6f;
    public float turnSpeed = 30f;

    Animator animator;
    Vector3 move;
    Quaternion rotation = Quaternion.identity;
    Rigidbody m_rigidbody;
    Transform m_transform;
    CapsuleCollider m_collider;

    private float horizontal = 0;
    private float vertical = 0;

    private bool isJumping = false;
    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_collider = GetComponent<CapsuleCollider>();
    }

    void FixedUpdate()
    {
        Run();
        Rotate();
        Jump();
    }

    // Update is called once per frame
    void Update()
    {
        Fallen();

        if (dead == false)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else if (dead == true)
        {
            m_rigidbody.velocity = Vector3.zero;
            horizontal = 0;
            vertical = 0;
        }
        

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isRunning = hasHorizontalInput || hasVerticalInput;

        if (isJumping && m_rigidbody.velocity.y < 0)
        {
            Physics.gravity = new Vector3(0, -20f, 0);
        }

        animator.SetBool("isRunning", isRunning);
    }

    void Run()
    {
        move = vertical * transform.forward * speed * Time.deltaTime;
        m_rigidbody.MovePosition(m_rigidbody.position + move);
    }

    void Rotate()
    {
        float rotation = horizontal * turnSpeed * Time.deltaTime;
        m_rigidbody.rotation *= Quaternion.Euler(0, rotation, 0);
    }

    void Jump()
    {
        if (Input.GetButton("Jump") && dead == false)
        {
            if (!isJumping)
            {
                isJumping = true;
                isGrounded = false;

                animator.SetBool("isJumping", isJumping);
                animator.SetBool("isGrounded", isGrounded);

                m_rigidbody.velocity = Vector3.zero;
                m_rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else 
                return;
        }
            
        
    }
    void Fallen()
    {
        if (m_transform.position.y < -1)
            dead = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isJumping)
            {
                isJumping = false;
                isGrounded = true;

                animator.SetBool("isJumping", isJumping);
                animator.SetBool("isGrounded", isGrounded);

                Physics.gravity = new Vector3(0, -9.8f, 0);
                m_rigidbody.velocity = Vector3.zero;
            }
            //animator.SetBool("isJumping", isJumping);
        }

        if(collision.gameObject.CompareTag("GameOver"))
        {
            dead = true;
        }
    }

}

