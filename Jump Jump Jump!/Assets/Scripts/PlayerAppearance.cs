using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppearance : MonoBehaviour
{
	int selected = 0;

	public GameObject Hat;
	public GameObject Head;
	public GameObject Eye;
	public GameObject SecondEye;
	public GameObject Mouth;
	public GameObject Body;
	public GameObject LHand;
	public GameObject RHand;
	public GameObject LLeg;
	public GameObject RLeg;



    // Start is called before the first frame update
    void Start()
    {
		//Hat = Resources.Load ("Player/BlueMorris/crowntest", typeof(GameObject)) as GameObject;
		//			# Works #			//
		selected = PlayerPrefs.GetInt ("Selected");

		if (selected == 1) {
			LLeg.gameObject.transform.position = new Vector3 (LLeg.gameObject.transform.position.x, (float)-0.575);
			RLeg.gameObject.transform.position = new Vector3 (RLeg.gameObject.transform.position.x, (float)-0.575);
			LHand.gameObject.transform.position = new Vector3 (LHand.gameObject.transform.position.x, (float)0.28);
			RHand.gameObject.transform.position = new Vector3 (RHand.gameObject.transform.position.x, (float)0.28);

			//	Hat.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/BlueMorris/crowntest03", typeof(Sprite)) as Sprite;
			Head.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/BlueMorris/BlueBody", typeof(Sprite)) as Sprite;
			Body.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/BlueMorris/BlueBody", typeof(Sprite)) as Sprite;
			Eye.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/BlueMorris/BlueEye", typeof(Sprite)) as Sprite;
			SecondEye.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/BlueMorris/BlueEye", typeof(Sprite)) as Sprite;
			Mouth.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/BlueMorris/BlueEye", typeof(Sprite)) as Sprite;
			LHand.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/BlueMorris/BlueLArm", typeof(Sprite)) as Sprite;
			RHand.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/BlueMorris/BlueLArm", typeof(Sprite)) as Sprite;
			LLeg.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/BlueMorris/BlueLFoot", typeof(Sprite)) as Sprite;
			RLeg.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/BlueMorris/BlueLFoot", typeof(Sprite)) as Sprite;
		}
		if (selected == 2) {
			LLeg.gameObject.transform.position = new Vector3 (LLeg.gameObject.transform.position.x, (float)-0.575);
			RLeg.gameObject.transform.position = new Vector3 (RLeg.gameObject.transform.position.x, (float)-0.575);
			LHand.gameObject.transform.position = new Vector3 (LHand.gameObject.transform.position.x, (float)0.28);
			RHand.gameObject.transform.position = new Vector3 (RHand.gameObject.transform.position.x, (float)0.28);

			//	Hat.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/BlueMorris/crowntest03", typeof(Sprite)) as Sprite;
			Head.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/PinkMorris/PinkBody", typeof(Sprite)) as Sprite;
			Body.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/PinkMorris/PinkBody", typeof(Sprite)) as Sprite;
			Eye.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/PinkMorris/PinkEye", typeof(Sprite)) as Sprite;
			SecondEye.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/PinkMorris/PinkEye", typeof(Sprite)) as Sprite;
			Mouth.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/PinkMorris/PinkEye", typeof(Sprite)) as Sprite;
			LHand.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/PinkMorris/PinkLArm", typeof(Sprite)) as Sprite;
			RHand.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/PinkMorris/PinkLArm", typeof(Sprite)) as Sprite;
			LLeg.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/PinkMorris/PinkLFoot", typeof(Sprite)) as Sprite;
			RLeg.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Player/PinkMorris/PinkLFoot", typeof(Sprite)) as Sprite;
		}

    }


}
