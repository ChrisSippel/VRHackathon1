﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetButton("Jump"))
        {
            transform.Translate(Vector3.up * Time.deltaTime);
        }
	}
}
