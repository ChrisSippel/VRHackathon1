using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class CharacterScript : MonoBehaviour {
    public float MoveSpeed;
    public float DeadZone = 0.25F;

    public event Action onFire;

    private bool hasController = false;
    public bool dead = false;

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
        if (dead)
        {
            return;
        }

        if (hasController)
        {
            MoveViaController(transform, MoveSpeed, DeadZone);

            float rightTriggerPulling = Input.GetAxis("RightTrigger");
            if (rightTriggerPulling == 1 && this.onFire != null)
            {
                this.onFire();
            }
        }
        else
        {
            MoveViaKeyboard(transform, MoveSpeed);
            if (Input.GetButtonDown("Fire1") && this.onFire != null)
            {
                this.onFire();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Rigidbody body = collision.gameObject.GetComponent<Rigidbody>();
            var vel = body.velocity;        //to get a Vector3 representation of the velocity
            float speed = vel.magnitude;    // to get magnitude

            if (speed >= 3 && speed <= 8)
            {
                dead = true;
                Debug.Log("DEAD");
                Transform child = transform.FindChild("Top");
                if (child != null)
                {
                    child.gameObject.AddComponent<Rigidbody>();
                    child.parent = null;
                }

                child = transform.FindChild("Middle");
                if (child != null)
                {
                    child.gameObject.AddComponent<Rigidbody>();
                    child.parent = null;
                }

                child = transform.FindChild("Bottom");
                if (child != null)
                {
                    child.gameObject.AddComponent<Rigidbody>();
                    child.parent = null;
                }

                child = transform.FindChild("CameraMover");
                if (child != null)
                {
                    child.gameObject.AddComponent<Rigidbody>();
                    child.parent = null;
                }

                GameObject arm = transform.FindChild("LeftArm").gameObject;
                arm.SetActive(false);

                arm = transform.FindChild("RightArm").gameObject;
                arm.SetActive(false);
            }
        }
    }

    private static void MoveViaKeyboard(Transform transform, float moveSpeed)
    {
        if (Input.GetButton("Up"))
        {
            Vector3 newForwardPosition = transform.position += Camera.main.transform.forward * moveSpeed * Time.deltaTime;
            newForwardPosition.Set(newForwardPosition.x, 6.14f, newForwardPosition.z);

            transform.position = newForwardPosition;
        }

        if (Input.GetButton("Down"))
        {
            Vector3 newForwardPosition = transform.position += (Camera.main.transform.forward * moveSpeed * Time.deltaTime) * -1;
            newForwardPosition.Set(newForwardPosition.x, 6.14f, newForwardPosition.z);

            transform.position = newForwardPosition;
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
        Vector3 newForwardPosition = transform.position += (Camera.main.transform.forward * Input.GetAxis("LeftStickYAxis") * moveSpeed * Time.deltaTime) * -1;
        newForwardPosition.Set(newForwardPosition.x, 6.14f, newForwardPosition.z);

        // Forward and Back
        transform.position = newForwardPosition;

        newForwardPosition = transform.position += Camera.main.transform.right * Input.GetAxis("LeftStickXAxis") * moveSpeed * Time.deltaTime;
        newForwardPosition.Set(newForwardPosition.x, 6.14f, newForwardPosition.z);

        // Left and Right
        transform.position = newForwardPosition;
    }
}
