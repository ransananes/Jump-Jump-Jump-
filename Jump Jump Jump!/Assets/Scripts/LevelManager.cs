using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class LevelManager : MonoBehaviour {

	public Transform player;
	public float additional = 1;

	//public Text currentscore;

	[Header("Side Walls")]
	public GameObject wallsPrefab;
	public float currentWallY;
	public float wallTall = 10f;
	public float distanceBeforeSpawn = 5f;
	public int initialWalls = 7;
	public List<GameObject> wallPool;

	[Header("Platforms")]
	private GameObject blockPrefab;
	public GameObject blockPrefabv0;
	public GameObject blockPrefabv1;
	public GameObject blockPrefabv2;
	public GameObject blockPrefabStart;
	int v = 0;
	private bool StartedDis = false;
	private bool Disabled = false;
	private bool filled = false;
	public List<float> timer;
	public List<float> timer2;

	public float currentBlockY;
	public float distanceBetweenBlocks = 3.5f;
	public float distanceBeforeSpawnBlock = 5f;
	public List<GameObject> blocksPool;
	public List<GameObject> blocksPool2;
	public List<bool> blocksPoolf;
	public List<bool> blocksPoolf2;
	private bool switcharray = false;
	// # Force Jump Effect (Fixed) #
	int prefablevel = 0;

	[Header("Coin")]
	public GameObject Coin;
	public List<GameObject> CoinList;
	public int coindes = 0;
//	bool prefabarray = false;
//	public Text Leveltext;
	[Header("Effect")]
	public GameObject ForceJumpEffect;
	public GameObject Snow;


	void Start()
	{
		InitCoins ();
		InitBlocks ();
		StartBlockFall ();
		StartTimers ();
	}
	private void Awake()
	{
		//InitSideWalls();
		//InitBlocks ();

	}
	private void LateUpdate()
	{
		if (player.position.y > 10f && StartedDis == false)
			StartedDis = true;
	}



	private void Update()
	{
		
		if (StartedDis == true) {
			if (Disabled == false) {
				blockPrefabStart.SetActive (false);
				Disabled = true;
			}

			for (int i = 0; i < 20; i++) {
				if (blocksPool [i].transform.position.y <= player.transform.position.y) {
					//StartCoroutine("ShortDelay");
					blocksPoolf [i] = true;
				}
				if (blocksPool2 [i].transform.position.y <= player.transform.position.y) {
					//StartCoroutine("ShortDelay");
					blocksPoolf2 [i] = true;
				}
				
			}
			for (int i = 0; i < 20; i++) {
				if (blocksPoolf [i] == true) {
					blocksPool [i].transform.Translate (Vector3.down * 1.32f * Time.deltaTime *additional);
					timer [i] += Time.deltaTime;
				}
				if (blocksPoolf2 [i] == true) {
					blocksPool2 [i].transform.Translate (Vector3.down * 1.32f * Time.deltaTime*additional);
					timer2[i] += Time.deltaTime;

				}

				if (timer [i] >= 4f)
					blocksPool [i].SetActive (false);
				if (timer2[i] >= 4f)
					blocksPool2[i].SetActive (false);
				
				StartCoroutine("Delay");
			}
		}


		
	

		if (currentWallY - player.position.y < distanceBeforeSpawn)
		{
			InitSideWalls ();
			SpawnSideWall();

		}

		if(currentBlockY - player.position.y < distanceBeforeSpawnBlock)
		{
			//InitBlocks ();
			switcharray=!switcharray;
			SpawnBlocks();
			InitCoins ();
			DestroyCoins ();
		}



	}
	IEnumerator Delay()
	{
		
			yield return new WaitForSeconds(3f);

	}
	IEnumerator ShortDelay()
	{

		yield return new WaitForSeconds(1.5f);

	}
	private void InitCoins()
	{
		float coinpos = currentBlockY;
			for (int i = 0; i < 15; i++) {
				
				


			Vector2 pos = new Vector2 (Random.Range (-6, 6), coinpos+1.5f*3);
				/*	if (i == 0) {
					Leveltext.transform.localPosition = pos;
					Leveltext.text = "1";
				}*/
				GameObject go = Instantiate (Coin, pos, Quaternion.identity, transform);
				CoinList.Add (go);
			coinpos += distanceBetweenBlocks;
			}
	}
	private void InitSideWalls()
	{
		for (int i = 0; i < initialWalls; ++i)
		{
			Vector2 pos = new Vector2(17.8f, currentWallY-10);
			GameObject go = Instantiate(wallsPrefab, pos, Quaternion.identity, transform);
			wallPool.Add(go);
			currentWallY += wallTall;
		}
	}

	private void InitBlocks()
	{
		if (filled == false) {
			for (int i = 0; i < 20; i++) {
				v = Random.Range (1, 4);
				switch (v) {
				case 1:
					blockPrefab = blockPrefabv0;
					break;
				case 2:
					blockPrefab = blockPrefabv1;
					break;
				case 3:
					blockPrefab = blockPrefabv2;
					break;
				}


				Vector2 pos = new Vector2 (Random.Range (-7, 7), currentBlockY);
			/*	if (i == 0) {
					Leveltext.transform.localPosition = pos;
					Leveltext.text = "1";
				}*/
				GameObject go = Instantiate (blockPrefab, pos, Quaternion.identity, transform);
				blocksPool.Add (go);
				currentBlockY += distanceBetweenBlocks;
			}
			for (int i = 0; i < 20; i++) {
				v = Random.Range (1, 4);
				switch (v) {
				case 1:
					blockPrefab = blockPrefabv0;
					break;
				case 2:
					blockPrefab = blockPrefabv1;
					break;
				case 3:
					blockPrefab = blockPrefabv2;
					break;
				}


				Vector2 pos = new Vector2 (Random.Range (-7, 7), currentBlockY);
				GameObject go = Instantiate (blockPrefab, pos, Quaternion.identity, transform);
				blocksPool2.Add (go);
				currentBlockY += distanceBetweenBlocks;
			} 
			filled = true;
		} else {
			if (prefablevel == 0) {
				ForceJumpEffect.GetComponent<Renderer> ().material = Resources.Load ("Materials/snowfoot", typeof(Material)) as Material;	
				blockPrefabv0 = Resources.Load ("Prefabs/SnowGroundV1", typeof(GameObject)) as GameObject;
				blockPrefabv1 = Resources.Load ("Prefabs/SnowGroundV2", typeof(GameObject)) as GameObject;
				blockPrefabv2 = Resources.Load ("Prefabs/SnowGroundV3", typeof(GameObject)) as GameObject;
				Snow.SetActive (true);
			}
			if (prefablevel == 2) {
				ForceJumpEffect.GetComponent<Renderer> ().material = Resources.Load ("Materials/dirtfoot", typeof(Material)) as Material;	
				blockPrefabv0 = Resources.Load ("Prefabs/DirtV1", typeof(GameObject)) as GameObject;
				blockPrefabv1 = Resources.Load ("Prefabs/DirtV2", typeof(GameObject)) as GameObject;
				blockPrefabv2 = Resources.Load ("Prefabs/DirtV3", typeof(GameObject)) as GameObject;
				additional = 1.2f;
			}
			if (prefablevel == 4) {
				ForceJumpEffect.GetComponent<Renderer> ().material = Resources.Load ("Materials/stonefoot", typeof(Material)) as Material;	
				blockPrefabv0 = Resources.Load ("Prefabs/StoneV1", typeof(GameObject)) as GameObject;
				blockPrefabv1 = Resources.Load ("Prefabs/StoneV2", typeof(GameObject)) as GameObject;
				blockPrefabv2 = Resources.Load ("Prefabs/StoneV3", typeof(GameObject)) as GameObject;
			}
			if (switcharray) {
				for (int i = 0; i < 20; i++) {
					Destroy (blocksPool [i]);
					v = Random.Range (1, 4);
					switch (v) {
					case 1:
						blockPrefab = blockPrefabv0;
						break;
					case 2:
						blockPrefab = blockPrefabv1;
						break;
					case 3:
						blockPrefab = blockPrefabv2;
						break;
					}



					Vector2 pos = new Vector2 (Random.Range (-7, 7
					
					
					
					), currentBlockY);
					GameObject go = Instantiate (blockPrefab, pos, Quaternion.identity, transform);
					blocksPool [i] = go;
					currentBlockY += distanceBetweenBlocks;

				}
				//prefabarray = true;
			}
			else {
				for (int i = 0; i < 20; i++) {
					Destroy (blocksPool2 [i]);
					v = Random.Range (1, 4);
					switch (v) {
					case 1:
						blockPrefab = blockPrefabv0;
						break;
					case 2:
						blockPrefab = blockPrefabv1;
						break;
					case 3:
						blockPrefab = blockPrefabv2;
						break;
					}


					Vector2 pos = new Vector2 (Random.Range (-7, 7), currentBlockY);
					GameObject go = Instantiate (blockPrefab, pos, Quaternion.identity, transform);
					blocksPool2 [i] = go;
					currentBlockY += distanceBetweenBlocks;
				}
				//prefabarray = false;
			}

		}
	}

	private void DestroyCoins()
	{
		for(int i =coindes;i<CoinList.Count;i++)
			{
			if(CoinList[i] != null)
			if (player.position.y - 30f > CoinList [i].transform.position.y) {
				Destroy (CoinList [i]);
				coindes = i;
			}
			}
	}

	private void SpawnSideWall()
	{
		if (wallPool.Count > 10)
			Clearwallpool ();
		wallPool[0].transform.position = new Vector2(17.8f, currentWallY-10f);
		currentWallY += wallTall;

		GameObject temp = wallPool[0];
		wallPool.RemoveAt(0);
		wallPool.Add(temp);

	}
				/*private void SpawnCoins()
				{
		float coinpos = currentBlockY;

					for (int i = 0; i < 10; i++)
					{
			CoinList [i].transform.position = new Vector3 (Random.Range (-6, 6), Random.Range (coinpos, coinpos+30),transform.position.z);
			coinpos += distanceBetweenBlocks;
					}
				}*/
	private void SpawnBlocks()
	{
		//DestroyUselessGrounds ();
		ClearBlockFall ();
		ResetTimers ();
		if (currentBlockY > 200 && prefablevel < 2) {
			InitBlocks ();
			prefablevel++;
			return;
		} else if (currentBlockY > 400 && prefablevel < 4) {
			InitBlocks ();
			prefablevel++;
			return;
		}
		else if (currentBlockY > 800 && prefablevel < 6) {
			InitBlocks ();
			prefablevel++;
			return;
		}
		/*Clearblockpool ();

		if (200 < currentBlockY && prefablevel == 0) {
			blockPrefabv0 = Resources.Load ("Prefabs/SnowGroundV1", typeof(GameObject)) as GameObject;
			blockPrefabv1 = Resources.Load ("Prefabs/SnowGroundV2", typeof(GameObject)) as GameObject;
			blockPrefabv2 = Resources.Load ("Prefabs/SnowGroundV3", typeof(GameObject)) as GameObject;
			filled = false;
			InitBlocks ();
		  
			prefablevel++;
			}
*/
			
		
		


		//Clearblockpool ();
//		Debug.Log ("Spawning Blocks~ ");
		//if (currentscore.ToString ().Equals ("Score: 20"))
		//	blockPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Environment/Platform_score100.prefab", 
		//		typeof(GameObject)) as GameObject; 
		//	EditorUtility.SetDirty (blockPrefab);


		//blockPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Environment/SnowGround.prefab",typeof(GameObject)) as GameObject;

		/*if (blocksPool.Count > 10 ) //clears blockpool
			Clearblockpool ();
		

		if (currentWallY > 100 && currentWallY < 220) {

			blockPrefab = Resources.Load ("Prefabs/SnowGround", typeof(GameObject)) as GameObject;
		

		}
		if(currentWallY > 129 && currentWallY < 249)
			ForceJumpEffect.GetComponent<Renderer> ().material = Resources.Load ("Materials/snowfoot", typeof(Material)) as Material;

		if (currentWallY > 249) {
			blockPrefab = Resources.Load ("Prefabs/SandGround", typeof(GameObject)) as GameObject;
		
		}
		if(currentWallY > 270)
			ForceJumpEffect.GetComponent<Renderer> ().material = Resources.Load ("Materials/sandfoot", typeof(Material)) as Material;
		*/

		//InitBlocks();
	//	InitBlocks ();


		//	blocksPool[0].transform.position = new Vector2(Random.Range(-5, 5), currentBlockY);
		//currentBlockY += distanceBetweenBlocks;


			if (switcharray == true) {
		//	Debug.Log ("x"+blocksPool[0].transform.position.x +"y " +blocksPool[0].transform.position.y);
				for (int i = 0; i < blocksPool.Count; i++)
			{
			//	blocksPool[i] = Resources.Load ("Prefabs/SnowGroundV3", typeof(GameObject)) as GameObject;

				blocksPool [i].transform.position = new Vector3 (Random.Range (-7, 7), currentBlockY,transform.position.z);
				//blocksPool[i].transform. //= Resources.Load ("Prefabs/SnowGroundV1", typeof(GameObject)) as GameObject;
				currentBlockY += distanceBetweenBlocks;
			}

		} else {

		
				for (int i = 0; i < blocksPool.Count; i++) {
				
			//	blocksPool2[i] = Resources.Load ("Prefabs/SnowGroundV3", typeof(GameObject)) as GameObject;
				blocksPool2 [i].transform.position = new Vector3 (Random.Range (-7, 7), currentBlockY,transform.position.z);
				currentBlockY += distanceBetweenBlocks;

				}
			}





	}
	private void Clearblockpool() // save cpu usage
	{
		if (switcharray == false) {
			for (int i = 0; i < blocksPool.Count; i++)
				GameObject.Destroy (blocksPool [i]);
			blocksPool.RemoveRange (0, blocksPool.Count);
		} else {
			for (int i = 0; i < blocksPool2.Count; i++)
				GameObject.Destroy (blocksPool2 [i]);
			blocksPool2.RemoveRange (0, blocksPool2.Count);
		}
	}
	private void Clearwallpool()
	{
		for (int i = 1; i < wallPool.Count-10; i++) 
			GameObject.Destroy (wallPool [i]);
		wallPool.RemoveRange (1, wallPool.Count-10);
	}
	void StartBlockFall()
	{
		for(int i =0;i<20;i++)
			blocksPoolf.Add (false);
		for(int i =0;i<20;i++)
			blocksPoolf2.Add (false);
	}
	void ClearBlockFall ()
	{
		if(switcharray)
		for (int i = 0; i < 20; i++)
			blocksPoolf[i] = false;
		else
		for (int i = 0; i < 20; i++)
			blocksPoolf2[i] = false;
	}

	void StartTimers ()
	{
		for (int i = 0; i < 20; i++)
			timer.Add (0);
		for (int i = 0; i < 20; i++)
			timer2.Add (0);
	}
	void ResetTimers()
	{
		if (switcharray)
			for (int i = 0; i < 20; i++) {
				timer [i] = 0;
				blocksPool [i].SetActive (true);
			}
		else
			for (int i = 0; i < 20; i++) {
				timer2 [i] = 0;
				blocksPool2[i].SetActive (true);

			}
	}
	void DestroyUselessGrounds()
	{
		string[] Destroyable = new string[]{"GroundV1(Clone)","GroundV2(Clone)","GroundV3(Clone),GrassCliffRight"};


		foreach (string name in Destroyable) {
				GameObject go = GameObject.Find (name);
				//if the tree exist then destroy it
				if (go)
					Destroy (go.gameObject);
			}
		}


}
