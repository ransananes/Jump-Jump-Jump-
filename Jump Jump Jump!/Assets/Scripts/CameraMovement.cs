using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour {

	//public GameManager gameManager;
	//public Text textSpeed;

	//private float speed;
	//public float speedMultiple = .05f;


	public Transform target;
	public GameObject cameraObj ;
	public Vector3 specificVector;
	public float smoothSpeed = 10f;
	//private float distance;

	public float startLimmit = 2f;
	//public float maxDistanceBeforeLose = 5f;
	//private string maxspeed = "x 4";

	//private float timer;

	void Start()
	{
		//timer = 0;
		cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
		Screen.SetResolution (1280, 720, true);

	}

	void Update()
	{
		specificVector = new Vector3(cameraObj.transform.position.x, target.transform.position.y+3f, cameraObj.transform.position.z);
		cameraObj.transform.position = specificVector;
	}
	void FixedUpdate () {
		//distance = target.position.y - transform.position.y;

		if (target.position.y < startLimmit) {
			return;
		}
		/*if (distance < -maxDistanceBeforeLose)
		{
			gameManager.GameOver();
		
		}
		*/
	/*	else if (distance > 1)
		{
			
			targetPosition = new Vector3(0, target.position.y, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, targetPosition, distance*Time.deltaTime);
		}
		else
		{

			targetPosition = new Vector3(0, transform.position.y + speed, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
		}

		timer += Time.deltaTime;
		textSpeed.text = "x " + (int)(1 + (Time.timeSinceLevelLoad) / 60);
		if (!maxspeed.ToString ().Equals (textSpeed.ToString ()))
			speed = (1 + (Time.timeSinceLevelLoad) / 60) * speedMultiple;
		*/
	}

}
