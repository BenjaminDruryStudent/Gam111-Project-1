using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float MaxTimeSeconds = 8;
	public int Turns = 10;
	float turnBetweenTime;
	bool Player1 = true;
	public float PlaySpeed = .5f;
    float RoundTime;

    public GameObject Player1Puppet;
    public GameObject Player2Puppet;
    PuppetCON Player1PuppetCON;
    PuppetCON Player2PuppetCON;

	//camera stuff
	public GameObject PlayerCON;
	public GameObject camera;
	int cameraTarget = 1;
	Vector3 PlayerConOffset;
	public float CamLerpSpeed = .1f;
	public float CamLerpZoom = .1f;
	public float cameraLengthPlayer = -2.5f;
	public float cameraLengthWorld = -4;

	//UI Vars
	public Text UIroundTimer;

    //RunTurn Varables
    bool runTurnState = false;
    float timeToRun = 0;

	// Use this for initialization
	void Start () {
		SetTimeAndTurns (MaxTimeSeconds,Turns);
		SetTime (0);

		UIroundTimer.text = Turns.ToString();

        Player1PuppetCON = Player1Puppet.GetComponent<PuppetCON>();
        Player1PuppetCON.turn = true;
        Player2PuppetCON = Player2Puppet.GetComponent<PuppetCON>();
        Player2PuppetCON.turn = false;

		PlayerConOffset = PlayerCON.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Turn"))
		{
			TurnEnd ();
		}
		CameraUpdate ();
	}

    // Fixed Update runs when physics runs
    void FixedUpdate()
    {
        RunTurn();
        RoundTime += Time.fixedDeltaTime;
    }

	void SetTimeAndTurns(float _time,int _turns)
	{
        MaxTimeSeconds = _time;
        Turns = _turns;
		turnBetweenTime = MaxTimeSeconds / Turns;
	}
	void TurnEnd()
	{
		if (Player1 == true) {
			//any transitions from player one to player two go here.
			Player1 = false;
			Player1PuppetCON.Turn (false);
			Player2PuppetCON.Turn (true);
			cameraTarget = 2;
			return;
		}
		if (Player1 != true) {
			//any transitons from player 2 to player one go here.			           
			Player2PuppetCON.Turn (false);
            Player1 = true;
            timeToRun = turnBetweenTime;
            runTurnState = true;
            RunTurn();
			cameraTarget = 0;
			return;
		}			
	}
    void RunTurn()
    {
        if (runTurnState != true)
            SetTime(0);
        if (runTurnState == true)
        {
            SetTime(PlaySpeed);
            timeToRun -= Time.fixedDeltaTime;            
            if (timeToRun <= 0)
            {
                runTurnState = false;
				Player1PuppetCON.Turn (true);
				Turns--;
				UIroundTimer.text = Turns.ToString();
				cameraTarget = 1;
            }                
        }
    }
	void SetTime(float _intime)
	{
		Time.timeScale = _intime;
		if (Time.timeScale > 0)
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
	}
	void CameraUpdate()
	{
		if (cameraTarget == 0) {
			PlayerCON.transform.position = Vector3.Lerp (PlayerCON.transform.position,(Vector3.zero + PlayerConOffset),CamLerpSpeed);
				return;
		}
		if (cameraTarget == 1) {
			PlayerCON.transform.position = Vector3.Lerp (PlayerCON.transform.position,(Player1Puppet.transform.position + PlayerConOffset), CamLerpSpeed);
				return;
		}
		if (cameraTarget == 2) {
			PlayerCON.transform.position = Vector3.Lerp (PlayerCON.transform.position,(Player2Puppet.transform.position + PlayerConOffset),CamLerpSpeed);
				return;
			}
	}
}