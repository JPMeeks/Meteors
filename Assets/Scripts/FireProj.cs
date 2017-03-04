using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// James P Meeks
// jpm4447@g.rit.edu
// shoots the bullets created by projectile manager

public class FireProj : MonoBehaviour 
{
	// variables
	private ProjectileManager projMngr; // bullet manager instance
	private Movement movement;			// player movement

	public GameObject player;
	public bool shoot = false;

	// Use this for initialization
	void Start () 
	{
		projMngr = gameObject.GetComponent<ProjectileManager> ();
		// error control
		if (projMngr == null) 
		{
			Debug.Log ("FireProj: Projectile Manager Error");
			Debug.Break ();
		}
		if (projMngr == null) 
		{
			Debug.Log ("FireProj: Player Error");
			Debug.Break ();
		}

		movement = player.GetComponent<Movement> ();
		if (projMngr == null) 
		{
			Debug.Log ("FireProj: Movement Error");
			Debug.Break ();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if (Input.GetKeyUp(KeyCode.Space)) 
		{
			shoot = true;
		}

		if (shoot) 
		{
			shoot = false;
			projMngr.FireProj(movement.position, movement.GetDirection() * 5.0f);
		}
	}
}

