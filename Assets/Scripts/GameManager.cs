using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float MaxTimeSeconds = 10;
	public int Turns = 10;
	float turnBetweenTime;
	bool Player1 = true;
	public float PlaySpeed = 1;
	float timescale = 0;

	//varables for CycleTime.
	bool playCycle = false;
	bool cycleTimeForward = true;
	float cycleTimeTimelineMax;
	float cycleTimeTimeline;

	// Use this for initialization
	void Start () {
		SetTimeAndTurns (MaxTimeSeconds,Turns);
		SetTime (0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Turn"))
		{
			TurnEnd ();
		}
		CycleTime();
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
			Debug.Log ("Player 1 ends turn");
			return;
		}
		if (Player1 != true) {
			//any transitons from player 2 to player one go here.
			Debug.Log ("Player 2 ends turn");
			Player1 = true;
			TriggerCycleTime ();
		}			
	}
	void TriggerCycleTime()
	{
		playCycle = true;
		cycleTimeTimelineMax = turnBetweenTime / 2;
		cycleTimeTimeline = 0;
	}

	void CycleTime()
	{		
		if (playCycle != true)
		{
			SetTime (0);
			return;
		}
		if (playCycle == true)
		{			
			cycleTimeTimeline += Time.deltaTime;

			if (cycleTimeForward == true)
			{
				timescale = Mathf.Lerp (0, PlaySpeed, cycleTimeTimeline / cycleTimeTimelineMax);
				if (timescale == PlaySpeed) {
					cycleTimeForward = false;
					cycleTimeTimeline = 0;
				}
			}

			if (cycleTimeForward != true)
			{
				timescale = Mathf.Lerp (PlaySpeed, 0, cycleTimeTimeline / cycleTimeTimelineMax);
				if (timescale == 0) {
					cycleTimeForward = true;
					cycleTimeTimeline = 0;
					playCycle = false;
				}
			}
			SetTime (timescale);
		}
	}

	void SetTime(float _intime)
	{
		Time.timeScale = _intime;
		if (Time.timeScale > 0)
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
	}
}