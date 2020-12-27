using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour {
    public Text textHighScore;
    int highscore = 0;
    // Use this for initialization
    void Start () {
		Invoke ("UpdatePoints", 2f);

    }

    // Update is called once per frame
    void Update () {
      //  textHighScore.text = "" + highscore;

    }
	void UpdatePoints()
	{
		highscore = PlayerPrefs.GetInt("highscore");

		textHighScore.text = "" + highscore;
	}
}
