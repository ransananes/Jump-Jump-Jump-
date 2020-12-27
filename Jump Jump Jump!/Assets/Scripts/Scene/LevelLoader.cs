using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class LevelLoader : MonoBehaviour
{
	public GameObject LoadingScreen;
	public Slider slider;
	public AudioSource A;
	public AudioClip Load;
	public void LoadLevel(int sceneindex)
	{
		
		//operation.progress;
		A.PlayOneShot(Load,1f);

		StartCoroutine(LoadASynchronously(sceneindex));

	}

	IEnumerator LoadASynchronously(int sceneindex)
	{
		Time.timeScale = 1;

		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneindex);

		LoadingScreen.SetActive (true);

		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress / .9f);
			slider.value = progress;
			yield return null;
		}
		
	}
}
