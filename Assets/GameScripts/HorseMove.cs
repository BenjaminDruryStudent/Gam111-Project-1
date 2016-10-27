using UnityEngine;
using System.Collections;

public class HorseMove : MonoBehaviour {

	public GameObject manager;
	GameManager gameManager;
	public Vector3 direction;
	public float speed;
	float gametime;

	void Start()
	{
		gameManager = manager.GetComponent<GameManager> ();
		gametime = gameManager.MaxTimeSeconds;
	}
		
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (direction * (speed / gametime));
	}
}
