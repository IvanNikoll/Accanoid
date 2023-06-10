using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject clouds;
    private GameObject MainPanel;
    public GameObject TopScorePanel;
    public GameObject SettingsPanel;
    public Data DataScript;
    public TextMeshProUGUI[] TopScore;
    void Start()
    {
        DataScript = GameObject.Find("Data").GetComponent<Data>();
        for(int i = 0; i < TopScore.Length; i++)
        {
            TopScore[i].SetText("1: " + (DataScript.test[i].ToString()) + " Points");
        }

        clouds = GameObject.Find("BG2");
        MainPanel = GameObject.Find("MainPanel");
        TopScorePanel = GameObject.Find("TopScorePanel");
        TopScorePanel.SetActive(false);
        SettingsPanel = GameObject.Find("SettingsPanel");
        SettingsPanel.SetActive(false);
    }
    void Update()
    {
        clouds.transform.Translate(new Vector2(0.2f, 0)*Time.deltaTime);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowTopScorePanel()
    {
        MainPanel.SetActive(false);
        TopScorePanel.SetActive(true);
    }

    public void ShowSettings()
    {
        MainPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }
    
    public void BackButton()
    {
        SettingsPanel.SetActive(false);
        TopScorePanel.SetActive(false);
        MainPanel.SetActive(true);
    }
}
