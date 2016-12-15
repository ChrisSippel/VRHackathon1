﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour {
    public float MoveSpeed;
    public float DeadZone = 0.25F;

    private bool hasController = false;

    // Use this for initialization
    void Start ()
    {
        List<string> joysticks = new List<string>(Input.GetJoystickNames());
        foreach(string joyStick in joysticks)
        {
            if (!string.IsNullOrEmpty(joyStick) && joyStick.Contains("Controller"))
            {
                hasController = true;
                return;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (hasController)
        {
            MoveViaController(transform, MoveSpeed, DeadZone);
        }
        else
        {
            MoveViaKeyboard(transform, MoveSpeed);
        }
    }

    private static void MoveViaKeyboard(Transform transform, float moveSpeed)
    {
        if (Input.GetButton("Up"))
        {
            transform.position += Camera.main.transform.forward * moveSpeed * Time.deltaTime;
        }

        if (Input.GetButton("Down"))
        {
            transform.position += (Camera.main.transform.forward * moveSpeed * Time.deltaTime) * -1;
        }

        if (Input.GetButton("Right"))
        {
            transform.position += Camera.main.transform.right * moveSpeed * Time.deltaTime;
        }

        if (Input.GetButton("Left"))
        {
            transform.position += (Camera.main.transform.right * moveSpeed * Time.deltaTime) * -1;
        }
    }

    private static void MoveViaController(Transform transform, float moveSpeed, float deadZone)
    {
        // Forward and Back
        transform.position += (Camera.main.transform.forward * Input.GetAxis("LeftStickYAxis") * moveSpeed * Time.deltaTime) * -1;

        // Left and Right
        transform.position += Camera.main.transform.right * Input.GetAxis("LeftStickXAxis") * moveSpeed * Time.deltaTime;
    }
}
