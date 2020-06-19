using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 6f;
    public float turnSpeed = 30f;

    Animator animator;
    Vector3 move;
    Quaternion rotation = Quaternion.identity;
    Rigidbody m_rigidbody;

    private float horizontal = 0;
    private float vertical = 0;

    private bool isJumping = false;
    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
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
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isRunning = hasHorizontalInput || hasVerticalInput;

        if (isJumping && m_rigidbody.velocity.y < 0)
        {
            m_rigidbody.mass = 3;
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
        if (Input.GetButtonDown("Jump"))
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

                m_rigidbody.mass = 1;
                m_rigidbody.velocity = Vector3.zero;
            }
            //animator.SetBool("isJumping", isJumping);
        }
    }

}

