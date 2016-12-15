using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShoot : MonoBehaviour {

	public float weaponRange = 50f;

	private Camera fpsCam;
	private LineRenderer laserLine;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);

	// Use this for initialization
	void Start () {
		laserLine = GetComponent<LineRenderer> ();
		fpsCam = GetComponentInParent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
