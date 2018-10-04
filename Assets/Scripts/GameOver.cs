using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Button restartButton;
    public Button mainMenuButton;
    public GameObject text;
    private Text scoreGT;

	// Use this for initialization
	void Start ()
	{
	    Button restartBtn = restartButton.GetComponent<Button>();
	    Button mainMenuBtn = mainMenuButton.GetComponent<Button>();
        restartBtn.onClick.AddListener(RestartTaskOnClick);
	    mainMenuBtn.onClick.AddListener(MainMenuTaskOnClick);
	    scoreGT = text.GetComponent<Text>();
	    scoreGT.text = "Your Score: " + Basket.score;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void RestartTaskOnClick()
    {
        SceneManager.LoadScene("_Scene_0");
    }

    void MainMenuTaskOnClick()
    {
        SceneManager.LoadScene("_Scene_Main");
    }
}
