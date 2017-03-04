using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// James Meeks
// jpm4447@g.rit.edu
// controls movement either through the use of user controlled movement, or randomly moves a sprite

public class Movement : MonoBehaviour 
{
	// variables
	public bool directed;
	public bool rand;
	public bool useSlow; // if true will slow the object when the forward button is not being pressed
	public bool randomSpawn;
	public Vector3 position;
	public Vector3 velocity = new Vector3 (1f, 0f, 0f);
	
	public float speed = 0.0f;
	public float speedIncrement = 10.0f;
	public float maxSpeed = 50.0f;
	public float slowDown = 0.97f;
	public float angle = 0.0f;

	private float height;
	private float width;
	private Camera cam;

	// Use this for initialization
	void Start () 
	{
		// gets viewport height and width
		cam = Camera.main;

		height = 2f * cam.orthographicSize;
		width = height * cam.aspect;

		// sets speed and velocity for non-directed objects
		if (rand == true && directed == false && randomSpawn == true) 
		{
			speed = Random.Range (1.0f, 5.0f);
			velocity = new Vector3 (-1.0f, 0.0f, 0.0f);
			position = new Vector3(Random.Range(-width/2,width/2), Random.Range(-height/2,height/2), 0.0f);
			angle = Random.Range(0.0f, 360.0f);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		if (directed) 
		{
			directedMovement ();
		} 
		else if (rand) 
		{
			
			randomMovement();
		}

		checkBounds (-(height/2), (height/2), -(width/2), (width/2));
			
		transform.rotation = Quaternion.Euler (0.0f, 0f, angle);

		position += transform.rotation * velocity * speed * Time.deltaTime;
			
		transform.position = position;
	}

	void directedMovement(){
	
		if (Input.GetKey (KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
		{
			speed += speedIncrement * Time.deltaTime;
		} 
		else if(useSlow == true)
		{
			speed *= slowDown;
		}
		
		if (speed > maxSpeed)
		{
			speed = maxSpeed;
		} 
		else if (speed < 0.01f) 
		{
			speed = 0.0f;
		}
		
		if (Input.GetKey (KeyCode.D)|| Input.GetKey(KeyCode.RightArrow)) 
		{
			angle -= 90.0f * Time.deltaTime * 2;
		} 
		else if (Input.GetKey (KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow)) 
		{
			angle += 90.0f * Time.deltaTime * 2;
		}
	
	}

	// sets the speed for random movement
	void randomMovement()
	{
		
		speed = 1.0f;
	
	}

	// uses the camera to set the bounds around the board and wraps objects back around the board
	void checkBounds(float bottom, float top, float left, float right){
	
		if(position.x > right+1)
		{
			position.x = left-1;
		}
		else if(position.x < left-1)
		{
			position.x = right+1;
		}
		
		if(position.y > top+1)
		{
			position.y = bottom-1;
		}
		else if(position.y < bottom-1)
		{
			position.y = top+1;
		}
	
	}

	public Vector3 GetDirection()
	{
		return Vector3.Normalize (transform.rotation * velocity);
	}
}
