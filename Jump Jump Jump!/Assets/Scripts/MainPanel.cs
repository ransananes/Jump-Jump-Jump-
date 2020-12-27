using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MainPanel : MonoBehaviour {
   public bool SFXmuted;
	public bool BGMmuted;

    public string soundtype;
	public string musictype;

	public bool mutedtf;
	public string muted;
    public AudioClip Music;

    public AudioSource MusicSource;

	public GameObject SoundManager;

	public AudioSource A;
	public AudioClip Pressed;

	public GameObject SettingsPanel;
	public GameObject InformationPanel;
	public Button SettingsEnabled;
	public Button PlayOnline; // PlayOnline Button
	public Button Shop; // Shop Button

	public bool NoSound;
	public Text Mute;
	//
	public Text textHighScore;
	public Text VersionText;
	Vector3 StartSizeVer;
	int toggle = 1;

	//coins
	public Text CurrentCoins;
	public GameObject coins;
	//Settings
	public Slider Volume;
	public Toggle tut;

	//LeaderBoards Update

    void Awake()
    {
	//	SFXCheck();
	//	BGMCheck();
			      

    }
    // Use this for initialization
    void Start () {
		//Debug.Log("FUCK U");
	/*	Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
			var dependencyStatus = task.Result;
			if (dependencyStatus == Firebase.DependencyStatus.Available) {
				
				// Create and hold a reference to your FirebaseApp,
				// where app is a Firebase.FirebaseApp property of your application class.
			//	var app = Firebase.FirebaseApp.DefaultInstance;
		
				// Set a flag here to indicate whether Firebase is ready to use by your app.
			} else {
				UnityEngine.Debug.LogError(System.String.Format(
					"Could not resolve all Firebase dependencies: {0}", dependencyStatus));
				// Firebase Unity SDK is not safe to use here.
			}
		});*/
		int lb = PlayerPrefs.GetInt ("LB");
		if (lb == 0) {
			PlayerPrefs.SetInt ("highscore", 0);
			PlayerPrefs.SetInt ("LB", 1);
			InformationPanel.SetActive (true);
		}
		textHighScore.text = "" + PlayerPrefs.GetInt("highscore");

		
		int Tut=PlayerPrefs.GetInt ("TUT");
		if (Tut == 1)
			tut.isOn = true;
		else
			tut.isOn = false;
		Volume.value = PlayerPrefs.GetFloat ("Volume");

		//transitionAnim.SetTrigger ("end");

		//Debug.Log ("RUN");
		PlayOnline.interactable = false;
		//Shop.interactable = false; -- shop works rn
		SettingsPanel.gameObject.SetActive(false);
        MusicSource.clip = Music;
		CheckIfAllMuted ();
		MUTEALL ();
		int BeforeCoins = PlayerPrefs.GetInt("Coins");
		CurrentCoins.text = "" + BeforeCoins;
		/*if (mutedtf == false)
		{
			//MusicSource.UnPause();
			//Should change button image
			AudioListener.volume = 1;
		}
		else 
		{
			MusicSource.Pause ();			//Should change button image
		}
		*/
		AudioListener.volume = Volume.value;

    }

    // Update is called once per frame
    void Update () {
		if (toggle >= 0 && toggle < 10)
		{
			toggle += 1;
			VersionText.transform.localScale += new Vector3(0.02f, 0.02f, 0);
		}
		else if (toggle < 0)
		{
			toggle += 1;
			VersionText.transform.localScale -= new Vector3(0.02f, 0.02f, 0);
		}
		else {
			toggle = -10;
		}
		//float newScale = Mathf.Lerp(1, 100, Time.deltaTime / 50);
	//	VersionText.transform.localScale=new Vector3(newScale,newScale,1);
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

	public void CloseInfo()
	{
		InformationPanel.SetActive (false);
	}
	/*  public void MuteSound()
    {
        if(SFXmuted == false)
        {
			PlayerPrefs.SetString("Sound", "disabled");
            SFXmuted = true;

            //Should change button image
        }
        else
        {
			PlayerPrefs.SetString("Sound", "enabled");
            SFXmuted = false;

            //Should change button image
        }
        

    }
    public void MuteBGM()
    {
		if(BGMmuted == false)
		{
			PlayerPrefs.SetString("Music", "disabled");
			BGMmuted = true;
			MusicSource.Pause ();

			//Should change button image
		}
		else
		{
			PlayerPrefs.SetString("Music", "enabled");
			BGMmuted = false;
			MusicSource.UnPause ();

			//Should change button image
		}
    }
    public void SFXCheck()
    {
		soundtype = PlayerPrefs.GetString("Sound");

		if (soundtype == "enabled")
        {
			SFXmuted = false;
        }
		else if (soundtype == "disabled")
        {
			SFXmuted = true;
        }
    }
    
	public void BGMCheck()
	{
		musictype = PlayerPrefs.GetString("Music");

		if (musictype == "enabled")
		{
			BGMmuted = false;
		}
		else if (musictype == "disabled")
		{
			BGMmuted = true;
		}
	}
	public void ShowAboutInformation()
	{
		AboutInformation.gameObject.SetActive(true);
		AboutEnabled.interactable = false;
		SettingsEnabled.interactable = false;



	}
	public void CloseAboutInformation()
	{
		AboutInformation.gameObject.SetActive(false);
		AboutEnabled.interactable = true;
		SettingsEnabled.interactable = true;

	}
	*/
	public void ShowSettings()
	{
		A.PlayOneShot (Pressed, 1f);
		SettingsPanel.gameObject.SetActive(true);
		SettingsEnabled.interactable = false;
	}
	public void CloseSettings()
	{
		A.PlayOneShot (Pressed, 1f);
		SettingsPanel.gameObject.SetActive(false);
		SettingsEnabled.interactable = true;	
	}
	public void MUTEALL()	// will be edited later
	{

		if (NoSound == false) {
			AudioListener.volume = 0;
			PlayerPrefs.SetString("Sound", "disabled");
			Mute.text = "UNMUTE";
			NoSound = true;
		} else {

			AudioListener.volume = Volume.value;
			PlayerPrefs.SetString("Sound", "enabled");
			Mute.text = "MUTE";
			NoSound = false;
		}
	}
	public void OnChangeVolume()
	{
		PlayerPrefs.SetFloat("Volume",Volume.value);
		AudioListener.volume = Volume.value;
	}
	public void OnChangeToggle()
	{
		if(tut.isOn)
			PlayerPrefs.SetInt ("TUT", 1);
		else
			PlayerPrefs.SetInt ("TUT", 0);
	}


}
