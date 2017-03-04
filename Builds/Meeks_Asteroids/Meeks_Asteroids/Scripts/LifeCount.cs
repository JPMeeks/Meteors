using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// James Meeks
// jpm4447@g.rit.edu
// sets the life bar up, controls the score, and allows exiting the game

public class LifeCount : MonoBehaviour 
{
	public int life = 3;
	public int score = 0;
	public GameObject lifeImage;
	public GameObject player;

	private List<GameObject> lifeList; 

	private float height;
	private float width;
	private Camera cam;

	// Use this for initialization
	void Start () 
	{
		lifeList = new List<GameObject> ();

		cam = Camera.main;

		height = 2f * cam.orthographicSize;

		for (int x = 0; x < life; x++) 
		{
			GameObject temp = (GameObject)Instantiate (lifeImage);
			Vector3 position = new Vector3 ((0 - 1) + x, (height/2) -1, 0f);
			temp.transform.position = position;

			lifeList.Add (temp);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (life < 3) 
		{
			Destroy (lifeList[life]);
			//lifeList.RemoveAt (life);

		}

		if (life == 0) 
		{
			Time.timeScale = 0;
			Destroy (player);
		}
	}

	void OnGUI()
	{
		GUILayout.Box ("" + score);
		GUI.Label (new Rect (1.0f, 1.0f, 1.0f, 1.0f), "" + score);
	}
}
