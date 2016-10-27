using UnityEngine;
using System.Collections;

public class PuppetCON : MonoBehaviour {

    public bool turn = false;
	public GameObject turnToken;

	void Start()
	{
		Turn (turn);
	}

	public void Turn(bool _in)
	{
		turn = _in;
		turnToken.SetActive (_in);
	}
}
