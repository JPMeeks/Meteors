using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// James Meeks
// jpm4447@g.rit.edu
// is used to check collision between certain sets of objects: players check against meteors, meteors against players and bullets

public class CollisionDetection : MonoBehaviour 
{
	public float radius;
	public Vector3 position;
	public GameObject manager;

	public Color32 colorBase;
	public Color32 colorChange;
	public bool collision; 

	private GameObject[] interactableObjects;
	private List<CollisionDetection> colliderObjects;
	private Movement movement;
	private SpriteRenderer sprite;

	// Use this for initialization
	void Start () 
	{
		manager = GameObject.Find ("GameManager");

		colliderObjects = new List<CollisionDetection> ();

		movement = gameObject.GetComponent<Movement> ();
		// error control
		if (movement == null) 
		{
			Debug.Log ("EROOR 1");
			Debug.Break ();
		}

		// get all collidable objects

		if (gameObject.tag == "Player") 
		{
			// gets the sprite renderer for a parented object
			sprite = gameObject.GetComponentInChildren<SpriteRenderer> ();
			// error control
			if (sprite == null) 
			{
				Debug.Log ("EROOR 1");
				//Debug.Break ();
			}

			// gets other collidable objects and converts them down into their CollisionDetection class
			interactableObjects = GameObject.FindGameObjectsWithTag ("Meteor");
			Debug.Log ("This works " + interactableObjects.Length);

			foreach (GameObject g in interactableObjects) 
			{
				colliderObjects.Add(g.GetComponent<CollisionDetection> ());
			}
		}


		if (gameObject.tag == "Meteor") 
		{
			// gets the sprite renderer for an unparented object
			sprite = gameObject.GetComponent<SpriteRenderer> ();
			// error control
			if (sprite == null) 
			{
				Debug.Log ("EROOR 1");
				//Debug.Break ();
			}

			interactableObjects = GameObject.FindGameObjectsWithTag ("Player");

			foreach (GameObject g in interactableObjects) 
			{
				colliderObjects.Add(g.GetComponent<CollisionDetection> ());
			}
		}

		if (gameObject.tag == "Bullet") 
		{
			// gets the sprite renderer for an unparented object
			sprite = gameObject.GetComponent<SpriteRenderer> ();
			// error control
			if (sprite == null) 
			{
				Debug.Log ("EROOR 1");
				//Debug.Break ();
			}

			interactableObjects = GameObject.FindGameObjectsWithTag ("Meteor");

			foreach (GameObject g in interactableObjects) 
			{
				colliderObjects.Add(g.GetComponent<CollisionDetection> ());
			}
		}

	}

	// Update is called once per frame
	void Update () 
	{
		// get position
		position = movement.position;
		bool colliding = false;

		foreach(CollisionDetection cd in colliderObjects)
		{
			collision = checkIntersect (position, radius, cd.position, cd.radius);

			if (collision == true && gameObject.tag == "Bullet") 
			{
				meteorSplit(cd);
				colliding = true;
				break;
			}

			if (collision == true && gameObject.tag == "Meteor") 
			{
				LoseLife (cd);
				colliding = true;
				break;
			}
		}

		if (colliding == true) 
		{
			sprite.color = colorChange;
		} 
		else 
		{
			sprite.color = colorBase;
		}
	}

	private bool checkIntersect(Vector3 pos1, float rad1, Vector3 pos2, float rad2)
	{
		// set distances
		float distX = pos2.x - pos1.x;
		float distY = pos2.y - pos1.y;

		float magnitude = Mathf.Sqrt (distX * distX + distY * distY);

		return magnitude < rad1 + rad2;
	}

	// destroy a meteor and psilits it into chunks before destroying the bullet that destroyed it
	private void meteorSplit(CollisionDetection cd)
	{
		Spawner spawn = manager.GetComponent<Spawner> ();
		LifeCount lifeC = manager.GetComponent<LifeCount> ();

		if (cd.gameObject.name == "meteorMedium_1(Clone)" || cd.gameObject.name == "meteorMedium_2(Clone)") 
		{
			spawn.GenerateLargeMeteor ();
			lifeC.score += 200;
		} 
		else 
		{
			spawn.GenerateSmallMeteor (cd.position, cd.movement.GetDirection(), cd.movement.angle);
			lifeC.score += 100;
		}

		// adds to score


		colliderObjects.Remove (cd);
		Destroy (cd.gameObject);
		Destroy (this.gameObject);
	}

	private void LoseLife(CollisionDetection cd)
	{
		LifeCount lifeC = manager.GetComponent<LifeCount> ();

		lifeC.life--;

		Destroy (this.gameObject);
	}
}