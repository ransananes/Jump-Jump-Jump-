using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
	public GameObject PausePanel;
	public GameObject Panel;
	public GameObject HTPPanel;
	public Button pausebt;
	public Text SecondsText;
	public AudioSource A;
	public AudioClip Pressed;
    void Start()
    {
		SecondsText.gameObject.SetActive (false);
    }
	// Update is called once per frame
	void Update()
	{

	}
	public void PauseGame()
	{
		A.PlayOneShot (Pressed, 1f);
		PausePanel.SetActive(true);
		Time.timeScale = 0;
		pausebt.interactable = false;
		Panel.gameObject.SetActive(false);
		HTPPanel.SetActive (false);

	}
	public void UnPauseGame()
	{
		A.PlayOneShot (Pressed, 1f);

		SecondsText.gameObject.SetActive (true);


		PausePanel.SetActive(false); 
		Panel.SetActive (true);

		StartCoroutine(getReady());



	}

	IEnumerator getReady()    
	{

		SecondsText.text = "3";    
		yield return StartCoroutine (WaitForRealSeconds(0.5f));

		SecondsText.text  = "2";    
		yield return StartCoroutine (WaitForRealSeconds(0.5f));

		SecondsText.text  = "1";    
		yield return StartCoroutine (WaitForRealSeconds(0.5f));

		SecondsText.text  = "GO";    
		yield return StartCoroutine (WaitForRealSeconds(0.5f));

		SecondsText.text  = "";

		Time.timeScale = 1;
		pausebt.interactable = true;

	}

	IEnumerator WaitForRealSeconds (float waitTime) {
		float endTime = Time.realtimeSinceStartup+waitTime;

		while (Time.realtimeSinceStartup < endTime) {
			yield return null;
		}
	}
    
}
