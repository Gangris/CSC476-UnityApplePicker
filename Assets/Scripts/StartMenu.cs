using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Button startButton;
    public Button closeButton;
	// Use this for initialization
	void Start ()
	{
	    Button startBtn = startButton.GetComponent<Button>();
	    Button closeBtn = closeButton.GetComponent<Button>();
        startBtn.onClick.AddListener(StartTaskOnClick);
        closeBtn.onClick.AddListener(ExitTaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartTaskOnClick()
    {
        SceneManager.LoadScene("_Scene_0");
    }

    void ExitTaskOnClick()
    {
        Application.Quit();
    }
}
