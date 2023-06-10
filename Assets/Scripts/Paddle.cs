using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private GameManager GameManagerScript;
    private float paddleSpeed;
    Rigidbody2D paddleRB;
    public BoxCollider2D colliderML;
    public BoxCollider2D colliderLL;
    public BoxCollider2D colliderMR;
    public BoxCollider2D colliderRR;
    public bool paddleBlocked = false;
    private void Awake()
    {
        paddleRB = gameObject.GetComponent<Rigidbody2D>();
        paddleSpeed = 10f; // paddle speed multiplier
        colliderLL = GameObject.Find("ZoneLL").GetComponent<BoxCollider2D>();
        colliderML = GameObject.Find("ZoneML").GetComponent<BoxCollider2D>();
        colliderMR = GameObject.Find("ZoneMR").GetComponent<BoxCollider2D>();
        colliderRR = GameObject.Find("ZoneRR").GetComponent<BoxCollider2D>();
        GameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    
    }
    void Update()
    {
        if (!GameManagerScript.isGameOver && GameManagerScript.isGameStarted)
        {



            if (Input.GetAxis("Horizontal") < 0)
            {
                paddleRB.AddForce(Vector2.left * paddleSpeed, ForceMode2D.Impulse);
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                paddleRB.AddForce(Vector2.right * paddleSpeed, ForceMode2D.Impulse);
            }

            // reduce paddle speed if player doesn't press the button
            if (paddleRB.velocity.x > 0 && !Input.GetKeyDown(KeyCode.RightArrow))
            {
                Vector2 retardingVector = paddleRB.velocity;
                paddleRB.AddForce(-retardingVector / 2, ForceMode2D.Impulse);
            }
            if (paddleRB.velocity.x < 0 && !Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Vector2 retardingVector = paddleRB.velocity;
                paddleRB.AddForce(-retardingVector / 2, ForceMode2D.Impulse);
            }

        }
        
                
    }
   
}
