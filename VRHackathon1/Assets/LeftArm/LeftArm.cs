using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArm : MonoBehaviour
{
    public Rigidbody snowballBullet;
    public Transform barrelEnd;
    public GameObject muzzleParticles;
    public AudioClip flareShotSound;
    public AudioClip noAmmoSound;
    public AudioClip reloadSound;
    public int bulletSpeed = 2000;
    public int maxSpareRounds = 5;
    public int spareRounds = 3;
    public int currentRound = 0;

    private Animator animator;
    private AudioSource audioSource;
    private bool hasAmmo = true;

    // Use this for initialization
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        animator.SetBool("HasAmmo", hasAmmo);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && !AnimatorIsPlaying())
        {
            if (hasAmmo)
            {
                Shoot();
            }
            else
            {
                animator.SetTrigger("Shoot");

                audioSource.clip = noAmmoSound;
                audioSource.PlayDelayed(0.33f);
            }
        }

        if (!hasAmmo &&
            Input.GetKeyDown(KeyCode.R) && 
            !AnimatorIsPlaying())
        {
            Reload();
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");
        audioSource.PlayOneShot(flareShotSound);
        hasAmmo = false;
        animator.SetBool("HasAmmo", hasAmmo);

        Rigidbody bulletInstance;
        bulletInstance = Instantiate(snowballBullet, barrelEnd.position, barrelEnd.rotation) as Rigidbody; //INSTANTIATING THE FLARE PROJECTILE

        bulletInstance.AddForce(barrelEnd.forward * bulletSpeed); //ADDING FORWARD FORCE TO THE FLARE PROJECTILE
    }

    private void Reload()
    {
        animator.SetTrigger("Reload");
        audioSource.PlayOneShot(reloadSound);
        hasAmmo = true;
        animator.SetBool("HasAmmo", hasAmmo);
    }

    private bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
