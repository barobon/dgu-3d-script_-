﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Movement : MonoBehaviour
{
    public static bool dead = false;
    public static bool boosted = false;
    public static bool restarted = false;
    public static float boostRate = 0.3f;
    public static float level = 1;   

    public float speed = 10f;
    private float backupSpeed;
    public float jumpForce = 6f;
    public float turnSpeed = 30f;
    public Transform startPos;

    Animator animator;
    Vector3 move;
    Quaternion rotation = Quaternion.identity;
    Rigidbody m_rigidbody;
    Transform m_transform;
    CapsuleCollider m_collider;
    AudioSource m_audioSource;

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
        startPos = transform;
        backupSpeed = speed;
        m_audioSource = GetComponent<AudioSource>();
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
        if (Input.GetKey("escape"))
            Application.Quit();
        Fallen();

        if (GameOver.score >= 10 * level)
        {
            speed = backupSpeed + backupSpeed * boostRate*level;
            level += 1;
            boosted = true;
        }

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
            RestartFunc();
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
    void LateUpdate()
    {
        if (boosted)
            boosted = false;
        if (restarted)
            restarted = false;
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
                m_audioSource.Play();

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
    void RestartFunc()
    {
        if (dead)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                speed = backupSpeed;
                dead = false;
                GameOver.score = 0;
                SceneManager.LoadScene("Start_Scene");
                restarted = true;
            }
        }
    }

}



