using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// James Meeks
// jpm4447@g.rit.edu
// spawns in meteors in the beginning of the game

public class Spawner : MonoBehaviour 
{
	public int meteorCount;

	public GameObject meteorLarge_1; // prefab for first level meteor
	public GameObject meteorLarge_2; // prefab for first level meteor
	public GameObject meteorLarge_3; // prefab for first level meteor

	public GameObject meteorSmall_1; // prefab for second level meteor
	public GameObject meteorSmall_2; // prefab for second level meteor

	public GameObject player; // requires a player so meteors don't spawn on top of him

	public List<GameObject> meteorList;

	// Use this for initialization
	void Start () 
	{
		meteorList = new List<GameObject> ();

		for (int x = 0; x < meteorCount; x++) 
		{
			GameObject temp = (GameObject)Instantiate (RandomMeteor());
			Vector3 position = new Vector3 (Random.Range (-3f,3f), Random.Range (-3f,3f), 0f);
			temp.transform.position = position;

			meteorList.Add (temp);
		}
	}

	public void GenerateLargeMeteor()
	{
		GameObject temp = (GameObject)Instantiate (RandomMeteor());
		Vector3 position = new Vector3 (Random.Range (-3f,3f), Random.Range (-3f,3f), 0f);
			
		temp.transform.position = position;

		meteorList.Add (temp);
	}

	// creates 2 small meteors when a big meteor is shot
	public void GenerateSmallMeteor(Vector3 position, Vector3 direction, float angle)
	{
		GameObject temp1 = (GameObject)Instantiate (meteorSmall_1);
		GameObject temp2 = (GameObject)Instantiate (meteorSmall_2);

		Movement move1 = temp1.GetComponent<Movement> ();
		Movement move2 = temp2.GetComponent<Movement> ();

		move1.position = position;
		move2.position = position;

		move1.angle = angle + 25.0f;
		move2.angle = angle - 25.0f;

		meteorList.Add (temp1);
		meteorList.Add (temp2);
	}

	// returns one of the three random meteors 
	private GameObject RandomMeteor()
	{
		int rando = Random.Range (1, 3);

		if (rando == 1) 
		{
			return meteorLarge_1;
		} 
		else if (rando == 2) 
		{
			return meteorLarge_2;
		} 
		else if (rando == 3) 
		{
			return meteorLarge_3;
		} 
		else 
		{
			return meteorLarge_1;
		}
	}
}
