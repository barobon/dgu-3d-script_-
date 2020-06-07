using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;

    Animator animator;
    Vector3 move;
    Quaternion rotation = Quaternion.identity;
    Rigidbody m_rigidbody;

    public float turnSpeed = 30f;

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
    }



}
