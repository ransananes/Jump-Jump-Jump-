using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    // Start is called before the first frame update
	//public Animator transitionAnim;
	public Button bt01;
	public Text bt01t;
	public Button bt02;
	public Text bt02t;
	public Button bt03;
	public Text bt03t;
	public Text Coinst;
	public Text NEC;

	public int selected;
	public int selectedbm;
	public int selectedpm; 
	public int Coins;

	public AudioSource A;
	public AudioClip Pressed;

	public float vectory;
	///public GameObject image;
    void Start()
    {
		vectory = NEC.transform.position.y;
		Coins = PlayerPrefs.GetInt("Coins");
		Coinst.text = "" + Coins;
		selected= PlayerPrefs.GetInt ("Selected");
		selectedbm = PlayerPrefs.GetInt ("BM");
		selectedpm = PlayerPrefs.GetInt ("PM");
		if(selected == 0)
			bt01t.text = "Selected";
			bt02t.text = "1000 Coins";
			bt03t.text = "1400 Coins";
				
		
		if (selectedbm == 1) {
			bt02t.text = "Select";
			if (selected == 1) {
				bt02t.text = "Selected";
			}
		}
		if (selectedpm == 1) {
			bt03t.text = "Select";
			if (selected == 2) {
				bt03t.text = "Selected";
			}
		}

    }

    // Update is called once per frame
    void Update()
    {
		if(NEC.IsActive())
			NEC.transform.Translate (Vector3.up * 10f * Time.deltaTime);    
		
}
	public void SelectBlueMorris()
	{
		A.PlayOneShot(Pressed, 1f);
		if (selectedbm == 0) { //Purchase
			if (Coins >= 1000) {
				PlayerPrefs.SetInt ("Coins", (Coins - 1000));
				Coins = Coins - 1000;
				Coinst.text = "" + Coins;
				selectedbm = 1;
				PlayerPrefs.SetInt ("BM", 1);


			} else {
				NEC.transform.position = new Vector3 (NEC.transform.position.x,vectory,NEC.transform.position.z);
				NEC.gameObject.SetActive (true);
				Invoke ("Delay",2f);
			}
	}
		if (selectedbm == 1) {
			
				bt02t.text = "Selected";
				PlayerPrefs.SetInt ("Selected", 1);
				bt01t.text = "Select";
			if (selectedpm == 0)
				bt03t.text = "1400 Coins";
			else
				bt03t.text = "Select";
				selected = 1;

				
		}
}
	public void SelectPinkMorris()
	{
		A.PlayOneShot(Pressed, 1f);
		if (selectedpm == 0) { //Purchase
			if (Coins >= 1400) {
				PlayerPrefs.SetInt ("Coins", (Coins - 1400));
				Coins = Coins - 1400;
				Coinst.text = "" + Coins;
				selectedpm = 1;
				PlayerPrefs.SetInt ("PM", 1);


			} else {
				NEC.transform.position = new Vector3 (NEC.transform.position.x,vectory,NEC.transform.position.z);
				NEC.gameObject.SetActive (true);
				Invoke ("Delay",2f);
			}
		}
		if (selectedpm == 1) {
			
			if (selectedbm == 0)
				bt02t.text = "1000 Coins";
			else
				bt02t.text = "Select";
				PlayerPrefs.SetInt ("Selected", 2);
				bt01t.text = "Select";
				bt03t.text = "Selected";
				selected = 2;


		}
	}
	public void SelectMorris()
	{
		A.PlayOneShot (Pressed, 1f);

		if (selected != 0) {
			bt02t.text = "Select";
			if (selectedpm == 0)
				bt03t.text = "1400 Coins";
			else
				bt03t.text = "Select";
			PlayerPrefs.SetInt ("Selected", 0);
			bt01t.text = "Selected";
			selected = 0;
		}
	}
	public void Delay()
	{
		NEC.gameObject.SetActive (false);
	}
}
