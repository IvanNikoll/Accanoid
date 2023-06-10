using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    private Rigidbody2D triggerRB;
    public bool isGameOver;
    private GameManager GameManagerScript;

    private void Start()
    {
        GameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManagerScript.isGameOver = true;
            GameManagerScript.isGameStarted = false;
            GameManagerScript.isLose = true; //change for testing  
            GameManagerScript.gameAudio.Stop();
            Destroy(collision.gameObject);
            StartCoroutine(GameManagerScript.GameOverCourutine());

        }   
    }


}
