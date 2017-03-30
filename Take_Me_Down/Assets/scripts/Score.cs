using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	public static int coins = 0;
	public static int diamonds = 0;

	public static int highestScore = 0;

	public static float score = 00;
	public GameObject scoreText;
	public GameObject yourScoreText;
	public GameObject pauseScoreText;
	public GameObject highScoreText;
	// Use this for initialization
	void Start () {
		highestScore = PlayerPrefs.GetInt ("highScore", 0);
	}

	// Update is called once per frame
	void Update () {
		if (GameController.PaddleMove == true){
			score += 5*Time.deltaTime;
		}
		int IntScore = (int)score;
		scoreText.GetComponent<TextMesh>().text = IntScore.ToString ("0#####");
		yourScoreText.GetComponent<TextMesh>().text = IntScore.ToString ();
		pauseScoreText.GetComponent<TextMesh>().text = IntScore.ToString ();
		highScoreText.GetComponent<TextMesh> ().text = highestScore.ToString ();

		if (IntScore > highestScore) {
			highestScore = IntScore;
			PlayerPrefs.SetInt ("highScore", Score.highestScore);
		}
			
			
	}
}
