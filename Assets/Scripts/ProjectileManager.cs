using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// James P Meeks
// jpm4447@g.rit.edu
// controls the total amount of bullets in the scene when attached to a game manager

public class ProjectileManager : MonoBehaviour 
{
	// variables
	public GameObject projectile; // prefab to be fired
	public float lifeTime = 1.0f; // time projectile is kept alive

	private int projShot = 0; // number of projectiles fired
	private List<GameObject> projPool; // list of current projectiles
	private List<float> lifePool;

	// Use this for initialization
	void Start () 
	{
		// initialize lists
		projPool = new List<GameObject> (); 
		lifePool = new List<float> ();

		// test prefab
		GameObject temp = Instantiate(projectile);
		Destroy (temp);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// check if projectile needs to be destroyed
		for (int x = 0; x < projShot; x++) 
		{
			if(Time.time > lifePool[x] + lifeTime)
			{
				// destroy the bullet, and remove it from its lists
				Destroy(projPool[x]);
				lifePool.RemoveAt (x);
				projPool.RemoveAt (x);

				// prevents index overflow
				--x;
				--projShot;
			}
		}
	}

	// creates and fires the projectile in a given direction at given force
	public void FireProj(Vector3 playerPosition, Vector3 direction)
	{
		// create necessary sections
		GameObject temp = Instantiate (projectile);
		Movement movement = temp.GetComponent<Movement> ();

		// get position and velocity of bullet
		movement.position = playerPosition;
		movement.velocity = direction;
		movement.speed = 1.0f;
		movement.useSlow = false;

		lifePool.Add (Time.time);
		projPool.Add (temp);
		++projShot;
	}
}
