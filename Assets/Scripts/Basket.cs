﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    [Header("Set Dynamically")]
    public Text scoreGT;

    public static int score;

    private Dictionary<string, int> scoreMap = new Dictionary<string, int>();

	// Use this for initialization
	void Start () {
	    GameObject scoreGO = GameObject.Find("ScoreCounter");
	    scoreGT = scoreGO.GetComponent<Text>();
	    score = 0;
	    scoreGT.text = score.ToString();

        scoreMap.Add("Apple(Clone)", 100);
        scoreMap.Add("AppleMedium(Clone)", 200);
        scoreMap.Add("AppleHard(Clone)", 400);
        scoreMap.Add("AppleImpossible(Clone)", 800);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Vector3 mousePos2D = Input.mousePosition;
	    mousePos2D.z = -Camera.main.transform.position.x;
	    Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
	    Vector3 pos = this.transform.position;
	    pos.x = mousePos3D.x;
	    this.transform.position = pos;
	}

    void OnCollisionEnter(Collision coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "Apple")
        {
            score += scoreMap[collidedWith.name];
            scoreGT.text = score.ToString();

            if (score > HighScore.score)
            {
                HighScore.score = score;
            }

            Destroy(collidedWith);
        }
    }
}
