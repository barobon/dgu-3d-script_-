using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    private Transform m_transform;
    private Transform player;
    public Transform startPos;
    public float Speed=4;
    private float dist;
    private float backupSpeed;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform;
        m_transform = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        backupSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, player.position)-2.39f;
        GameOver.enemyDist = dist;
        m_transform.Translate(Vector3.forward*Speed*Time.deltaTime);
        m_transform.LookAt(player.transform);
        if (Movement.boosted)
        {
            Speed = backupSpeed + backupSpeed * Movement.boostRate * Movement.level;
        }
        if (Movement.restarted)
            Speed = backupSpeed;
    }

}
