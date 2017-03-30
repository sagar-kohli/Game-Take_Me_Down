using UnityEngine;
using System.Collections;

public class DiamondCoin : MonoBehaviour {

	public static int diamondCount = 0;
	public static int coinCount = 0;

	public GameObject diamondText;
	public GameObject coinsText;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{
		diamondText.GetComponent<TextMesh> ().text = diamondCount.ToString ();
		coinsText.GetComponent<TextMesh> ().text = coinCount.ToString ();


		if (GameController.gameOver) {
			PlayerPrefs.SetInt ("diamonds", PlayerPrefs.GetInt ("diamonds", 0) + diamondCount);
			PlayerPrefs.SetInt ("coins", PlayerPrefs.GetInt ("coins", 0) + coinCount);
			GameController.gameOver = false;
		}
	}
}
