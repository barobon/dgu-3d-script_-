using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    private Transform m_transform;
    private Transform player;
    public Transform startPos;
    public float Speed;
    private float dist;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform;
        m_transform = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, player.position)-2.39f;
        GameOver.enemyDist = dist;
        m_transform.Translate(Vector3.forward*Speed*Time.deltaTime);
        m_transform.LookAt(player.transform);
    }

}
