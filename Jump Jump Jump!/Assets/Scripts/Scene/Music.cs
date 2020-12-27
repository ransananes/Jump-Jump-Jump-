using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour {

	void Awake ()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
		if (objs.Length > 1)
			Destroy(this.gameObject);

		DontDestroyOnLoad(this.gameObject);

	}

	void Update()
	{
	/*	if (SceneManager.GetActiveScene().name == "SceneName")
		{
			Destroy(this.gameObject);
		}
		*/
	}
}