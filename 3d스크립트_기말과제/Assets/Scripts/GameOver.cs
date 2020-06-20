using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private bool isDead = false;
    public static int score = 0;
    Animator animator;
    public Text ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isDead = Movement.dead;

        if (isDead)
        {
            animator.SetBool("isDead", isDead);
        }

        ScoreText.text = "Score: "+score.ToString();
    }
}
