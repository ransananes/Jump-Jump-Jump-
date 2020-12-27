using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour {
	
	//public Animator transitionAnim;
	public Animator panelGameOverAnim;
	public Text gameScore;
	public Text menuScore;

	public Button pausebt;
	public GameObject PausePanel;
    public GameObject Panel;
	public GameObject HTPPanel;
	public Toggle NeverDisplay;

	public bool Scored = false;

	public bool NoSound;


	public GameObject Player;
	void Start()
	{
	//	transitionAnim.SetTrigger ("end");
		if(PlayerPrefs.GetInt("TUT") == 0)
				HTPPanel.SetActive (false);
		PausePanel.gameObject.SetActive(false);
		Time.timeScale = 1;
		CheckIfAllMuted ();
		EDSound ();

	}
	void Update()
	{
		
	}

    public void GameOver()
	{
     //   GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false; ~ For Keyboard use
        //GameObject.FindGameObjectWithTag("Player").GetComponent<MovementButtons>().enabled = false;
		//menuScore.text = gameScore.text.ToString();

		panelGameOverAnim.SetTrigger("Open");
		if (panelGameOverAnim.GetBool ("Open") == true && Scored == false) {
			menuScore.text = gameScore.text.ToString();
			Scored = true;
			Player.SetActive (false);
			//Debug.Log (menuScore.text.ToString() + "/" +gameScore.text.ToString());
		
		}
		int x = Random.Range (1, 100);

		if (x > 35)
		if(Advertisement.IsReady())
			Advertisement.Show("video");
		pausebt.interactable = false;
		Panel.gameObject.SetActive(false);
	}



	public void CheckIfAllMuted()
	{
		string PPSound = PlayerPrefs.GetString("Sound");

		if (PPSound.ToString().Equals("disabled"))
		{
			NoSound = false;

			//Debug.Log ("NoSound = false");
		}
		else
		{
			NoSound = true;
			//Debug.Log ("NoSound = true");

		}
	}

	void EDSound ()
	{
		if (NoSound)
			AudioListener.volume = 	PlayerPrefs.GetFloat ("Volume");
		
		else
			AudioListener.volume = 0;
	}
	public void CloseHTP()
	{
		if (NeverDisplay.isOn) {
			PlayerPrefs.SetInt ("TUT", 0);
		}
		HTPPanel.SetActive (false);

	}
}
