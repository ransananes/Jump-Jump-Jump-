using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
	public GameObject Snow;
	public GameObject Player;
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Snow.transform.position = new Vector2 (0, Player.transform.position.y+10f);
    }
}
