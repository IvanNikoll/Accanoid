using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Ball : MonoBehaviour
{
    public float speed = 3f;
    private Vector2 startOffset; //offset for the ball idle position
    private Rigidbody2D ballRB;
    private GameObject paddleObj;
    private GameManager GameManagerScript;
    private Paddle PaddleScript;
    private bool isBallStarted; // checks whether the ball started moving or not 
    private void Awake()
    {
        ballRB= GetComponent<Rigidbody2D>();
        paddleObj = GameObject.Find("Paddle");
        PaddleScript = GameObject.Find("Paddle").GetComponent<Paddle>();

    }
    // Start is called before the first frame update
    void Start()
    {
        startOffset = new Vector2(0, 1);
        GameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && GameManagerScript.isGameStarted &&!isBallStarted) 
        {
            ballRB.AddForce(Vector2.up * speed);
            isBallStarted= true; 
        }
        if (!isBallStarted)
        {
            startOffset.x = paddleObj.transform.position.x;
            gameObject.transform.position = startOffset;
        }
        if (!GameManagerScript.isGameOver)
        {


            // ball speed limitations
            if (ballRB.velocity.y > 6)
            {
                Vector2 retardingVector = -ballRB.velocity.normalized;
                ballRB.AddForce(retardingVector);
            }
            if (ballRB.velocity.x > 6)
            {
                Vector2 retardingVector = -ballRB.velocity.normalized;
                ballRB.AddForce(retardingVector);
            }
            if (ballRB.velocity.x < -6)
            {
                Vector2 retardingVector = -ballRB.velocity.normalized;
                ballRB.AddForce(retardingVector);
            }
            if (ballRB.velocity.y < -6)
            {
                Vector2 retardingVector = -ballRB.velocity.normalized;
                ballRB.AddForce(retardingVector);
            }

        }
        if (GameManagerScript.isGameOver)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) // The paddle has got 4 colliders for various reflection of the ball 
    {
        if (collision.collider == PaddleScript.colliderLL)
        {
            ballRB.AddForce(Vector2.left*2);
        }
        if (collision.collider == PaddleScript.colliderML)
        {
            ballRB.AddForce(Vector2.left);
        }
        if (collision.collider == PaddleScript.colliderMR)
        {
            ballRB.AddForce(Vector2.right);
        }
        if (collision.collider == PaddleScript.colliderRR)
        {
            ballRB.AddForce(Vector2.right * 2);
        }
       
        
    }
    private void OnCollisionExit2D(Collision2D collision) // describes collision with blocks
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            Destroy(collision.gameObject);
            GameManagerScript.score++;
            GameManagerScript.gameAudio.PlayOneShot(GameManagerScript.addPoint);
            GameManagerScript.scoreText.SetText("Score: " + GameManagerScript.score.ToString());
        }
    }
}
