﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotator : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float RotateSpeed = 2f;
    public float maxRotation = 45f;
    public GameObject player;
    public int Health = 0;
    public Rigidbody snowballBullet;
    public float BulletSpeed;

    private GameObject turret;
    private float FOV = 180f;
    Vector3 m_lastKnownPosition = Vector3.zero;
    private Transform barrelEnd;
    private const float shootDelay = 2f;
    private static float shotDuration = 0;
    private CharacterScript character;

    // Use this for initialization
    void Start () {
        turret = transform.FindChild("Top").gameObject;
        barrelEnd = transform.FindChild("Top").FindChild("Cannon_2").FindChild("BarrelEnd");
        character = player.GetComponent<CharacterScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (LineOfSight(player.transform) &&
            !character.dead)
        {
            turret.transform.rotation = Quaternion.LookRotation(turret.transform.position - player.transform.position);

            shotDuration -= Time.deltaTime;
            if (shotDuration <= 0)
            {
                Shoot();
                shotDuration = shootDelay;
            }
        }
        else
        {
            //there is something obstructing the view
            turret.transform.rotation = Quaternion.Euler(0f, maxRotation * Mathf.Sin(Time.time * RotateSpeed), 0f);
        }
    }

    private void Shoot()
    {
        Rigidbody bulletInstance;
        bulletInstance = Instantiate(snowballBullet, barrelEnd.position, barrelEnd.rotation) as Rigidbody; //INSTANTIATING THE FLARE PROJECTILE

        bulletInstance.AddForce(barrelEnd.forward * BulletSpeed); //ADDING FORWARD FORCE TO THE FLARE PROJECTILE
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Health--;
        }

        if (Health < 0)
        {
            CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
            capsuleCollider.enabled = false;
            Rigidbody collisionRigidBody = collision.gameObject.GetComponent<Rigidbody>();

            foreach (Transform child in transform)
            {
                Rigidbody rigidBody = child.GetComponent<Rigidbody>();
                rigidBody.isKinematic = false;
                rigidBody.AddForce(collisionRigidBody.velocity, ForceMode.VelocityChange);
            }
        }
    }

    private bool LineOfSight(Transform target)
    {
        RaycastHit hit;
        bool withinFov = Vector3.Angle(target.position - turret.transform.position, turret.transform.forward) <= FOV;
        bool lineCast = Physics.Linecast(turret.transform.position, target.position, out hit);
        bool sameTarget = hit.collider.transform == target;
        if (withinFov &&
            lineCast &&
            sameTarget)
        {
            return true;
        }

        return false;
    }
}
