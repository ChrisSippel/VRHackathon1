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
    private float FOV = 60f;

    Vector3 m_lastKnownPosition = Vector3.zero;
    Quaternion m_lookAtRotation;

    // Use this for initialization
    void Start () {
        turret = GameObject.Find("Head");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (LineOfSight(player.transform))
        {
            if (m_lastKnownPosition != player.transform.position)
            {
                m_lastKnownPosition = player.transform.position;
                m_lookAtRotation = Quaternion.LookRotation(m_lastKnownPosition - turret.transform.position);
            }

            if (turret.transform.rotation != m_lookAtRotation)
            {
                turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, m_lookAtRotation, FollowSpeed * Time.deltaTime);
            }
        }
        else
        {
            // there is something obstructing the view
            transform.rotation = Quaternion.Euler(0f, maxRotation * Mathf.Sin(Time.time * RotateSpeed), 0f);
        }
    }

    private bool LineOfSight(Transform target)
    {
        RaycastHit hit;

        if (Vector3.Angle(target.position - transform.position, transform.forward) <= FOV &&
            Physics.Linecast(transform.position, target.position, out hit) &&
            hit.collider.transform == target)
        {
            return true;
        }

        return false;
    }
}
