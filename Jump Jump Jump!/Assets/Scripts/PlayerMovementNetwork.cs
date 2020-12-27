using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementNetwork : MonoBehaviour {

	//Joystick
	protected Joystick joystick;
	protected Joybutton joybutton;
	protected bool jump;

	//Player
	private bool facingRight;
	private bool isGrounded;
	public Transform groundCheck;
	private Animator animator;
	private Rigidbody2D rigidbody;

	//Sound
	public AudioSource JumpSound;
	public AudioSource SuperJumpSound;
	protected string soundcheck;
	protected bool soundon;

	//Effect
	public ParticleSystem SuperJumpEffect;
	public ParticleSystem MoveEffect;

	//Panel How To Play - Hide/Show
	public GameObject PanelHTP;
	protected bool HTP = true;

	//acceleration coming-soon
	//	private float acceleration = 1f;
	//	private float maxspeed = 20f;
	//	private float curspeed = 0;

	//gameover
	public GameManager gameManager;
	//textSpeed
	public Text textSpeed;
	public int textMultiplySpeed = 1;
	public float timerMultiply = 0;

	//score edit
	public ScoreManager scoreManager;
	public Animator panelGameOverAnim;
	int sumbonus = 0;
	int latesttransformposition = 0;
	int latestscore = 0;
	bool StartedDis = false;


	// Use this for initialization
	void Start () {

		//Define
		joystick = FindObjectOfType<Joystick> ();
		joybutton = FindObjectOfType<Joybutton> ();
		rigidbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator>();
		panelGameOverAnim = panelGameOverAnim.GetComponent<Animator> ();
		//Sound
		soundcheck = PlayerPrefs.GetString("Sound");
		if (soundcheck.ToString ().Equals ("disabled")) {
			soundon = false;

		} else {
			soundon = true;

		}




	}

	// Update is called once per frame
	void Update () {
		timerMultiply += Time.deltaTime;

		if (timerMultiply > 3 && textMultiplySpeed != 1) {
			timerMultiply = 0;
			textMultiplySpeed = textMultiplySpeed/2;
			textSpeed.text = " +"+textMultiplySpeed;
		}
		//SpeedMultiply (textMultiplySpeed);
		if (rigidbody.velocity.y <= -50)
			gameManager.GameOver ();
		isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		animator.SetBool("Grounded", isGrounded);
		//Debug.Log ("" +rigidbody.velocity.x);
		//	if (rigidbody.velocity.magnitude > maxspeed)
		//	if(joystick.Horizontal.Equals(0))
		//{
		//if(curspeed > acceleration) curspeed -= acceleration; 
		//if(curspeed < -acceleration) curspeed += acceleration;
		///curspeed += acceleration*joystick.Horizontal;
		//curspeed = Mathf.Clamp(curspeed,-maxspeed,maxspeed); // Clamps curSpeed

		rigidbody.velocity = new Vector2 (joystick.Horizontal*7f, rigidbody.velocity.y);
		float horizontal = joystick.Horizontal * 2f * 2f * Time.deltaTime;

		rigidbody.AddForce(new Vector2(horizontal, 0), ForceMode2D.Impulse);
		if (!jump && joybutton.Pressed) {
			jump = true;
			if (HTP) {
				PanelHTP.SetActive (false);
				HTP = false;
			}	
		}


		if (jump && !joybutton.Pressed ) {
			//rigidbody.isKinematic = true;
			jump = false;
		}

	}

	private void FixedUpdate()
	{
		if (jump && isGrounded && rigidbody.velocity.y < 1.8f && rigidbody.velocity.y > -1.8f) {
			//rigidbody.isKinematic = false;
			if (Mathf.Abs (joystick.Horizontal) > 0.7)
				rigidbody.velocity = Vector2.up * 18.5f * (Mathf.Abs (joystick.Horizontal)+1.3f); // # Change with velocity of the player (FIXED)
			else
				rigidbody.velocity = Vector2.up * 18.5f*1.1f;
			if (soundon && joystick.Horizontal > 0.5f || joystick.Horizontal < -0.5f) {
				SuperJumpSound.Play ();
				SuperJumpEffect.Play ();
				if (textMultiplySpeed != 8) {
					int x = Random.Range (0, 100);
					if (x < 100 - textMultiplySpeed*20) {
						textMultiplySpeed = textMultiplySpeed * 2;
						textSpeed.text = " +" + textMultiplySpeed;
					}
					timerMultiply = 0;

				}
			}
			else if (soundon && joystick.Horizontal < 0.5f || joystick.Horizontal > -0.5f)
				JumpSound.Play ();

		}
		if ((joystick.Horizontal > 0 && !facingRight) || (joystick.Horizontal < 0 && facingRight)) Flip();
		animator.SetFloat("Speed", Mathf.Abs(joystick.Horizontal));
		if (joystick.Horizontal != 0)
			MoveEffect.Play ();
		else
			MoveEffect.Stop ();
		//		Debug.Log ("" + rigidbody.velocity.y);

	}
	private void LateUpdate()
	{
		if (transform.position.y > 5f && StartedDis == false)
			StartedDis = true;
	}
	private void Flip()
	{
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Wall")
		{
			//Flip();
			//Vector2 rev = new Vector2(rb2D.velocity.x * bounceFactor, 0);
			//	rb2D.AddForce(rev, ForceMode2D.Impulse);
		}

		if(col.gameObject.tag == "Platform")
		{

			if (StartedDis == false)
				return;
			if(latestscore >((int)transform.position.y/3)+textMultiplySpeed-1 + sumbonus)
				return;
			if (latesttransformposition >= (int)transform.position.y/3)
				return;
			int score = ((int)transform.position.y / 3) + textMultiplySpeed-1+ sumbonus ;
			scoreManager.UpdateScore(score);
			latesttransformposition = (int)transform.position.y / 3 ;
			sumbonus += textMultiplySpeed;
			latestscore = score;
		}
	}
	public int SpeedMultiply(){
		return textMultiplySpeed;
	}
}
