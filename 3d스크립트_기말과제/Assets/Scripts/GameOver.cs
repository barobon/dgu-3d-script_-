using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private bool isDead = false;
    private bool played = false;
    public static int score = 0;
    public static float enemyDist = 0;
    Animator animator;
    public Text ScoreText;
    public Text EnemyDist;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        isDead = Movement.dead;
        animator.SetBool("isDead", isDead);
        ScoreText.text = "Score: "+score.ToString();
        EnemyDist.text = "Enemy Distance:" + enemyDist.ToString();
        
    }

    
}
