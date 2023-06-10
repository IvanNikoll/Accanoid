using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using System;
//using UnityEditor.UI;
using TMPro;
//using UnityEditor.Audio;
using System.Runtime.CompilerServices;
using Unity.IO;
using System.IO;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int finalScore;
    public float time = 100.0f;
    public bool isGameStarted = false;
    public bool isGameOver = false;
    bool gameoverStarted = false;
    public bool isLose;
    public Block blockPrefab;
    private int linesNumber = 3;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI helpText;
    public GameObject countdownPanel;
    public GameObject levelPanel;
    public GameObject GameOverPanel;
    public GameObject scorePanel;
    public GameObject starPanel;
    public AudioClip addPoint;
    public AudioClip countdownAudio;
    public AudioClip endOfRoundTimer10;
    public AudioClip gameOverLoseAudio;
    public AudioClip gameOverWinAudio;
    public AudioClip menuSound;
    public AudioSource gameAudio;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public Data DataScript;
    public GameObject DataObj;
    public bool isDataSaved = false;
    public void Awake()
    {
        DataObj = GameObject.Find("Data");
        DataScript = DataObj.GetComponent<Data>();
    }

    private void Start()
    {
        gameAudio = GetComponent<AudioSource>();
        
        // instantiate blocks for level 1
        const float step = 3.0f;
        int perLine = Mathf.FloorToInt(30f / step);
        for (int i = 0; i < linesNumber; i++)
        {
            for (int x = 0; x < perLine; x++)
            {
                Vector3 position = new(-15f + step * x, 6f + 3f * i, -1f);
                Instantiate(blockPrefab, position, Quaternion.identity);
            }
        }
        StartCoroutine(StartDelay());
    }
        IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2);
        levelPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        StartCoroutine(RoundCountdown());
        
        StopCoroutine(StartDelay());
    }
        IEnumerator RoundCountdown()
        {
            countdownPanel.SetActive(true);
            string[] countdownValues = new string[] {"3","2","1","GO"};
            gameAudio.PlayOneShot(countdownAudio);
            for (int i = 0; i <4; i++)
            {
                countdownText.SetText(countdownValues[i]);
                yield return new WaitForSeconds(1);
            }
            countdownPanel.SetActive(false);
            levelPanel.SetActive(false);
            isGameStarted = true;
            StartCoroutine(RoundTimer());
        }

    IEnumerator RoundTimer()
    {
        for (float i = time; i > 0; i--)
        {
            while (!isGameOver)
            {
                time--;
                float minutes = Mathf.FloorToInt(time / 60);
                float seconds = Mathf.FloorToInt(time % 60);
                timeText.text = string.Format("Time: {00}:{1:00}", minutes, seconds);
                if (time == 10)
                {
                    gameAudio.PlayOneShot(endOfRoundTimer10);
                }
                if(time == 0)
                {
                    gameAudio.Stop();
                    isLose = true;
                }
                yield return new WaitForSeconds(1);
            }
        }    
        
        
    }
    public IEnumerator GameOverCourutine()
    {
        if (isLose)
        {
            gameAudio.PlayOneShot(gameOverLoseAudio);
        }
        if (!isLose)
        {
            gameAudio.PlayOneShot(gameOverWinAudio);
        }
        yield return new WaitForSeconds(3);
        GameOverPanel.SetActive(true);
        gameAudio.PlayOneShot(menuSound);
        yield return new WaitForSeconds(0.3f);
        levelPanel.SetActive(true);
        gameAudio.PlayOneShot(menuSound);
        yield return new WaitForSeconds(0.5f);
        scorePanel.SetActive(true);
        gameAudio.PlayOneShot(menuSound);
        if (isLose)
        {
            int scoreCalc = 0;
            int scoreToCalc = score;
            for (int i = 0; i < scoreToCalc; i++)
            {
                scoreCalc++;
                gameAudio.PlayOneShot(addPoint);
                score--;
                finalScoreText.SetText("Score: " + scoreCalc.ToString());
                
                scoreText.SetText("Score: " + score.ToString());
                if(score == 0)
                {
                    scoreText.SetText("Score: ");
                }
                yield return new WaitForSeconds(0.1f);
            }

        }
        if (!isLose)
        {
            int scoreCalc = 0;
            if (score != 0)
            {
                int scoreToCalc = score;

                for (int i = 0; i < scoreToCalc; i++)
                {
                    scoreCalc++;
                    gameAudio.PlayOneShot(addPoint);
                    score--;
                    finalScoreText.SetText("Score: " + scoreCalc.ToString());

                    scoreText.SetText("Score: " + score.ToString());
                    if (score == 0)
                    {
                        scoreText.SetText("Score: ");
                    }
                    yield return new WaitForSeconds(0.1f);
                }
                int timeToCalc = (int)time;
                for (int i = 0; i < timeToCalc; i++)
                {
                    scoreCalc++;
                    gameAudio.PlayOneShot(addPoint);
                    time--;
                    finalScoreText.SetText("Score: " + scoreCalc.ToString());
                    float minutes = Mathf.FloorToInt(time / 60);
                    float seconds = Mathf.FloorToInt(time % 60);
                    timeText.text = string.Format("Time: {00}:{1:00}", minutes, seconds);
                    if (time <= 0)
                    {
                        scoreText.SetText("Time: ");
                    }
                    yield return new WaitForSeconds(0.05f);
                }
                finalScore = scoreCalc;
            }
        }
        yield return new WaitForSeconds(0.5f);
        
        if (!isLose && !isDataSaved)
        {
            starPanel.SetActive(true);
            star1.SetActive(true);
            if (finalScore > 40)
            {
                star2.SetActive(true);
            }
            if (finalScore > 60)
            {
                star3.SetActive(true);
            }
            gameAudio.PlayOneShot(menuSound);
            SaveData();
        }
        
        yield return null;
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void SaveData()
    {
        if(finalScore > DataScript.test[0])
        {
            if (DataScript.test[0] != 0)
            {
                DataScript.test[2] = DataScript.test[1];
                DataScript.test[1] = DataScript.test[0];
                
            }
            DataScript.test[0] = finalScore;
        }
        if(finalScore > DataScript.test[1] && finalScore < DataScript.test[0])
        {
            DataScript.test[2] = DataScript.test[1];
            DataScript.test[1] = finalScore;
        }
        if(finalScore > DataScript.test[2] && finalScore < DataScript.test[1])
        {
            DataScript.test[2] = finalScore;
        }

        isDataSaved = true;
        DataScript.Save();
        StopCoroutine(GameOverCourutine());
    }


    private void FixedUpdate()
    {
        if(time <= 0)
        {
            
            isGameOver = true;
            isGameStarted = false;
            if (!gameoverStarted)
            {
                StartCoroutine(GameOverCourutine());
                gameoverStarted= true;
            }
        }
        if (!isGameStarted)
        {
            helpText.SetText("Press Space button");
            
        }
         if (isGameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            helpText.gameObject.SetActive(false);
        }

    }
   
}

