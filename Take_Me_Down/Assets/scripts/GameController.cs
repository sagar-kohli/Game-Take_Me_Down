using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static bool PaddleMove = false;							//	should paddle start moving or not paddle will not start immidiately after level being loaded
	bool flag = false;
	public static bool gameOver = false;

	// Use this for initialization
	void Start () {
		PaddleMove = false;
		Timer.timerCountdown = 60f;
		PaddleScript.initiationFactor = 0.0f;
		PaddleScript.spikeNum = 0.0f;
		Score.score = 0.0f;
		gameOver = false;
		DiamondCoin.coinCount = 0;
		DiamondCoin.diamondCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!PaddleMove && !flag) {              //add (&& countdown.timerCountdown==60f)
			flag = true;
			StartCoroutine (waitBeforeStart ());
		}
	}

	IEnumerator waitBeforeStart(){
		yield return new WaitForSeconds (3f);
		PaddleMove = true;

	}
}