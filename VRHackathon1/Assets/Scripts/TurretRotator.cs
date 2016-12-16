using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotator : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float RotateSpeed = 2f;
    public float maxRotation = 45f;
    public GameObject player;

    private GameObject turret;
    private float FOV = 180f;
    private float turnRateRadians = 2 * Mathf.PI;

    Vector3 m_lastKnownPosition = Vector3.zero;
    Quaternion m_lookAtRotation;

    // Use this for initialization
    void Start () {
        turret = GameObject.Find("Top");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (LineOfSight(player.transform))
        {
            turret.transform.rotation = Quaternion.LookRotation(turret.transform.position - player.transform.position);
        }
        else
        {
            //there is something obstructing the view
            turret.transform.rotation = Quaternion.Euler(0f, maxRotation * Mathf.Sin(Time.time * RotateSpeed), 0f);
        }
    }

    private bool LineOfSight(Transform target)
    {
        RaycastHit hit;
        bool withinFov = Vector3.Angle(target.position - turret.transform.position, turret.transform.forward) <= FOV;
        if (withinFov &&
            Physics.Linecast(turret.transform.position, target.position, out hit) &&
            hit.collider.transform == target)
        {
            return true;
        }

        return false;
    }
}
