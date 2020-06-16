using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 6f;

    Animator animator;
    Vector3 move;
    Quaternion rotation = Quaternion.identity;
    Rigidbody m_rigidbody;

    public float turnSpeed = 30f;
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        m_rigidbody.MovePosition(transform.position + move * speed * Time.deltaTime);
        m_rigidbody.MoveRotation(rotation);
        Jump();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        move.Set(horizontal, 0f, vertical);
        move.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isRunning = hasHorizontalInput || hasVerticalInput;

        animator.SetBool("isRunning", isRunning);
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, move, turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredForward);
        //rotation = Quaternion.Slerp(transform.rotation, cam.rotation, turnSpeed * Time.deltaTime);
  
    }


    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                isJumping = true;
                animator.SetBool("isJumping", isJumping);
                //animator.SetBool("isRunning", false);
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
                animator.SetBool("isJumping", isJumping);
                m_rigidbody.velocity = Vector3.zero;
            }
            //animator.SetBool("isJumping", isJumping);
        }
    }

}

