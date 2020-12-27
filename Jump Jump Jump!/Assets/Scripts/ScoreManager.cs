using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text textScore;
    public int highscore;
    public Text textfall;

	public GameObject Morris;
	private PlayerMovement scriptparamter;


    void Start () {
        highscore = PlayerPrefs.GetInt("highscore");
        textScore.text = "Score: 0";
	}
	void Update()
	{

	}
	
	public void UpdateScore (int score) {


		textScore.text = "Score: "+ score ;
        if ((score) > highscore)
        {
            highscore = (score) ;
            PlayerPrefs.SetInt("highscore", highscore);
            textfall.text = "New Record!";
            return;
        }
		if ((score)  > 200 && score<500)
            textfall.text = "Nice!";
		if ((score)  > 500 && score<1000)
            textfall.text = "Over 500!";
		if ((score)  > 1000 && score <1300)
            textfall.text = "Excellent!";
		if ((score)  > 1300)
			textfall.text = "Oh My GAWD!";
        
    }

}
