using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

    public float Speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Up"))
        {
            transform.position += Camera.main.transform.forward * Speed * Time.deltaTime;
        }
        
        if (Input.GetButton("Down"))
        {
            transform.position += (Camera.main.transform.forward * Speed * Time.deltaTime) * -1;
        }

        if (Input.GetButton("Right"))
        {
            transform.position += Camera.main.transform.right * Speed * Time.deltaTime;
        }

        if (Input.GetButton("Left"))
        {
            transform.position += (Camera.main.transform.right * Speed * Time.deltaTime) * -1;
        }
    }
}
