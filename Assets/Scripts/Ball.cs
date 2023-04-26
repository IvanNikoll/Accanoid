using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 3f;
    private Vector2 startOffest; //offset for the ball idle position
    private bool isGameStarted = false;
    private Rigidbody2D ballRB;
    private GameObject paddleObj;
    private GameManager GameManagerScript;
    private Paddle PaddleScript;
    private void Awake()
    {
        ballRB= GetComponent<Rigidbody2D>();
        paddleObj = GameObject.Find("Paddle");
        PaddleScript = GameObject.Find("Paddle").GetComponent<Paddle>();

    }
    // Start is called before the first frame update
    void Start()
    {
        startOffest = new Vector2(0, 1);
        GameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isGameStarted) 
        {
            ballRB.AddForce(Vector2.up * speed);
            isGameStarted=true;
        }
        if (!isGameStarted)
        {
            startOffest.x = paddleObj.transform.position.x;
            gameObject.transform.position = startOffest;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
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
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            Destroy(collision.gameObject);
            GameManagerScript.score++;
        }
    }
}
