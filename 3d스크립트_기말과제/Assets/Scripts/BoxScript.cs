using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private bool clicked = false;
    private bool trap = false;
    private int score;
    public AudioSource scoreSfx;

    // Start is called before the first frame update
    void Start()
    {
        scoreSfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        clicked = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Player") && clicked == false)
        {
            scoreSfx.Play();
            clicked = true;
            GameOver.score++;
        }
    }
}
