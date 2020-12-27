using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
	
	//Joystick
	[Header("Joystick")]
	protected Joystick joystick;
	protected Joybutton joybutton;
	protected bool jump;

	//Player
	[Header("Player")]
	private bool facingRight;
	private bool isGrounded;
	public Transform groundCheck;
	private Animator animator;
	private Rigidbody2D rigidbody;

	//Sound
	[Header("Sound")]
	public AudioClip JumpSound;
	public AudioClip SuperJumpSound;
	public AudioClip Land;
	public AudioClip Wow;
	public AudioClip ScreamingSound;
	public AudioClip Whoopie;
	public AudioClip HighScoreSound;
	public AudioClip WooSound;
	public AudioClip WoahSound;
	protected string soundcheck;
	protected bool soundon;
	 public AudioSource audio;

	//Effect
	[Header("Effect")]
	public ParticleSystem SuperJumpEffect;
	public ParticleSystem MoveEffect;
	public ParticleSystem HighscoreEffect;
	public GameObject Trail;

	//acceleration
	private float acceleration = 1f;
	private float maxspeed = 10f;

	//gameover
	public GameManager gameManager;
	//textSpeed
	public Text textSpeed;
	public int textMultiplySpeed = 1;
	public float timerMultiply = 0;



	//score edit
	[Header("Score")]
	public ScoreManager scoreManager;
	public Animator panelGameOverAnim;
	int sumbonus = 0;
	int latesttransformposition = 0;
	int latestscore = 0;
	//highscore
	public int highscore=0;
	public Text HighscoreText;
	//bool StartedDis = false;


	//coins
	[Header("Coins")]
	public int coins = 0;
	public Text Coinstext;
	public Text DeathCoins;
	public AudioClip CoinPickup;

	//Wall Duplicate
	bool WallDuplicate;

	//Trail

	// Use this for initialization

	void Start () {
		
		//Define
		//audio = GetComponent<AudioSource>();
		joystick = FindObjectOfType<Joystick> ();
		joybutton = FindObjectOfType<Joybutton> ();
		  rigidbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator>();
		panelGameOverAnim = panelGameOverAnim.GetComponent<Animator> ();
		//Sound
		soundcheck = PlayerPrefs.GetString("Sound");
		highscore = PlayerPrefs.GetInt("highscore");

		if (soundcheck.ToString ().Equals ("disabled")) {
			soundon = false;

		} else {
			soundon = true;

		}
	//	Debug.Log ("HighScore"+highscore);




	}
	
	// Update is called once per frame
	void Update () {
	//	Debug.Log ("" + transform.position.x);
		if (HighscoreEffect.gameObject.activeSelf == false && highscore + 1 < latestscore) {
			HighscoreText.gameObject.SetActive (true);
			HighscoreEffect.gameObject.SetActive (true);
			audio.PlayOneShot (HighScoreSound,1f);
		}
		if (HighscoreText.IsActive ()) {
			HighscoreText.transform.localScale = new Vector3(Mathf.Lerp(HighscoreText.transform.localScale.x,0,Time.deltaTime),Mathf.Lerp(HighscoreText.transform.localScale.y,0,Time.deltaTime),0);
		}
		Coinstext.text = ""+coins;
		timerMultiply += Time.deltaTime;

		if (timerMultiply > 3 && textMultiplySpeed != 1) {
			timerMultiply = 0;
			textMultiplySpeed = textMultiplySpeed/2;
			textSpeed.text = " +"+textMultiplySpeed;
		}
		//SpeedMultiply (textMultiplySpeed);
		if (rigidbody.velocity.y <= -60) {
			gameManager.GameOver ();
			audio.PlayOneShot (ScreamingSound, 1f);
			int BeforeCoins = PlayerPrefs.GetInt("Coins");
			PlayerPrefs.SetInt("Coins", BeforeCoins+coins);
			DeathCoins.text = ""+coins;

		}
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
		if (joystick.Horizontal > 0 && acceleration < maxspeed) 
				acceleration++;
		else if(joystick.Horizontal < 0 && acceleration > -maxspeed) acceleration--;

	//	if(rigidbody.velocity.y != 0 || rigidbody.velocity.y < -5)
		//	rigidbody.velocity = new Vector2 (joystick.Horizontal*9f, rigidbody.velocity.y);
	
	//	else
		rigidbody.velocity = new Vector2 (Mathf.Lerp(rigidbody.velocity.x,Mathf.Abs(joystick.Horizontal)*acceleration*1.1f,Time.deltaTime*10f), rigidbody.velocity.y);

		//Debug.Log (""+rigidbody.velocity.x);
	//	float horizontal = joystick.Horizontal * 2f * 2f * Time.deltaTime;

	//	rigidbody.AddForce(new Vector2(horizontal, 0), ForceMode2D.Impulse);
		if (!jump && joybutton.Pressed) {
			jump = true;

		}


		if (jump && !joybutton.Pressed ) {
			//rigidbody.isKinematic = true;
			jump = false;
		}
		if (jump && isGrounded && rigidbody.velocity.y < 1.8f && rigidbody.velocity.y > -1.8f) {
			//rigidbody.isKinematic = false;
			float speed = 8f;
			if (rigidbody.velocity.x == 0)
				speed = 16f;

			rigidbody.velocity = Vector2.up * (Mathf.Abs(rigidbody.velocity.x)+speed)*1.1f * (Mathf.Abs (joystick.Horizontal)+1.1f); // # Change with velocity of the player (FIXED)

			if (rigidbody.velocity.y > 27f) {
				audio.PlayOneShot (SuperJumpSound, 1f);
				SuperJumpEffect.Play ();
				if (textMultiplySpeed != 8) {
					int x = Random.Range (0, 100);
					if (x < 100 - textMultiplySpeed * 20) {
						textMultiplySpeed = textMultiplySpeed * 2;
						textSpeed.text = " +" + textMultiplySpeed;
						int p = Random.Range (0, 2);
						if (p == 1)
							audio.PlayOneShot (Wow, 1f);
						else
							audio.PlayOneShot (Whoopie, 1f);
					} else
						audio.PlayOneShot (WooSound, 1f);
					timerMultiply = 0;

				}
				else audio.PlayOneShot (WoahSound, 1f);
			} else {
					audio.PlayOneShot (WoahSound, 1f);
				audio.PlayOneShot (JumpSound, 1f);
			}
		}
		animator.SetFloat("Speed", Mathf.Abs(joystick.Horizontal));
	}

	private void FixedUpdate()
	{
		if (isGrounded && joystick.Horizontal != 0) MoveEffect.Play();
		else MoveEffect.Stop();
		//Debug.Log ("" + Vector2.up);
	

		//Debug.Log ("" + rigidbody.velocity.y);
	/*	if (joystick.Horizontal != 0 || (rigidbody.velocity.y < 0 && rigidbody.velocity.y < -10f))
			MoveEffect.Play ();
		else
		*/ //	MoveEffect.Stop ();
//		Debug.Log ("" + rigidbody.velocity.y);

	}
	private void LateUpdate()
	{
		if ((joystick.Horizontal > 0 && !facingRight) || (joystick.Horizontal < 0 && facingRight)) Flip();

	}
	private void Flip()
	{
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
	private void EnableTrail()
	{
		Trail.SetActive (true);
	}
	private void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Wall")
		{
			Trail.SetActive (false);
		/*	audio.PlayOneShot (Land, 0.7f);
			float rbvy = (rigidbody.velocity.y) / 10;
			if (this.transform.position.y > 20f) {
				Vector2 rev = new Vector2 (rigidbody.velocity.x * 3f, rbvy);
				rigidbody.AddForce (rev, ForceMode2D.Impulse);
			}*/ 
			if (transform.position.x < -12f) {
				transform.position  = new Vector3(11.99f, transform.position.y,transform.position.z);
				Flip();
			}
			 if(transform.position.x > 12f) {
				transform.position  = new Vector3(-11.99f, transform.position.y,transform.position.z);
				Flip();

			}
			Invoke ("EnableTrail", 0.5f);
			/*
			if (WallDuplicate) {
				sumbonus = (int)(sumbonus * 1.1f);
				WallDuplicate = false;
				Invoke ("WallDuplicateBool",2.5f);
			}*/
		}

		if(col.gameObject.tag == "Platform")
		{
			
		/*	canPlayJumpLandingSound = true;
			if(!audio.isPlaying && canPlayJumpLandingSound == true && (rigidbody.velocity.y != 0 || rigidbody.velocity.y < -5))
			{
				canPlayJumpLandingSound = false;
				//audio.PlayOneShot(Land, 0.1f);
			}
			*/
			//if (StartedDis == false)
			//	return;
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
		if (col.gameObject.tag == "Coin") {
			coins++;
		//	Debug.Log ("Coin Touched");
			audio.PlayOneShot(CoinPickup,1f);
			Destroy (col.gameObject);
		}
	}
	public int SpeedMultiply(){
		return textMultiplySpeed;
	}
}
