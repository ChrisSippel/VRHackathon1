using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShoot : MonoBehaviour {

	public float weaponRange = 50f;
    public Transform laserPointer;

    private Camera fpsCam;
	private LineRenderer laserLine;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);

	// Use this for initialization
	void Start () {
		laserLine = GetComponent<LineRenderer> ();
		fpsCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Fire1"))
	    {
	        StartCoroutine(ShotEffect());

	        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
	        RaycastHit hit;
            laserLine.SetPosition(0, fpsCam.transform.position);

	        if (Physics.Raycast (rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
	        {
	            laserLine.SetPosition(1, hit.point);

	            MenuButton button = hit.collider.GetComponent<MenuButton>();

	            if (button != null)
	            {
	                button.SwitchLevel();
	            }
	        }
	        else
	        {
	            laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
	        }
	    }
	}

    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
