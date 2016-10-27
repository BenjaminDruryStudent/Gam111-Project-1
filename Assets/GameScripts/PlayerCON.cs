using UnityEngine;
using System.Collections;

public class PlayerCON : MonoBehaviour {

	public float cameraSpeed = 2;

    public GameObject cameraYaw;
    public GameObject cameraPitch;

	Vector3 cameraTargetPos;

	// Use this for initialization
	void Start () {
		cameraTargetPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		InputHandle ();
		//UpdateCamera();
	}
	void InputHandle()
	{
		if (Input.GetButton("Horizontal"))
		{
            cameraYaw.transform.Rotate(0, (Input.GetAxisRaw("Horizontal")*cameraSpeed),0);
		}
		if (Input.GetButton("Vertical"))
		{
            cameraPitch.transform.Rotate((Input.GetAxisRaw("Vertical")*cameraSpeed),0 ,0);
		}
		if (Input.GetButtonDown("Reset"))
			Application.LoadLevel (Application.loadedLevel);

		if (Input.GetButtonDown("Quit"))
            Application.Quit();
	}

	void UpdateCamera()
	{
		transform.position = Vector3.Lerp(transform.position,cameraTargetPos,.1f);
	}
}
