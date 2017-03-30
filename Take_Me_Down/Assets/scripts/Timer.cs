using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public static float timerCountdown = 60f;
	public GameObject TimerText;
	public GameObject PauseTimerText;

	// Update is called once per frame
	void Update () {
		if (GameController.PaddleMove == true) {
			timerCountdown -= Time.deltaTime;
		}
		int IntTimer = (int)timerCountdown;
		TimerText.GetComponent<TextMesh>().text = IntTimer.ToString ();
		PauseTimerText.GetComponent<TextMesh>().text = IntTimer.ToString ();
	}
}
