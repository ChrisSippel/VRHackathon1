using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 10 + 1f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
