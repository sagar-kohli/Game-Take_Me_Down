using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed = 4f;	//speed of the movement of the ball
	public GameObject particles;
	public GameObject gameOverPlane;
	public GameObject RHCDPanel;

	// Use this for initialization
	void Start () {
		gameOverPlane.SetActive (false);
		RHCDPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.PaddleMove == false)
			return;
		float width = Screen.width;

		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).position.x > width / 2) {
				transform.Translate (speed * Time.deltaTime, 0, 0);
			}
			if (Input.GetTouch (0).position.x < width / 2) {
				transform.Translate (-speed * Time.deltaTime, 0, 0);
			}
		}

		if (Application.isEditor || Application.isWebPlayer) {
			float inputs = Input.GetAxis ("Horizontal");
			transform.Translate (inputs * speed * Time.deltaTime,0,0);
		}

		if (transform.position.x < -3.12f) {
			Vector3 pos = transform.position;
			pos.x = 3.12f;
			transform.position = pos;
		} 
		else if (transform.position.x > 3.12f) {
			Vector3 pos = transform.position;
			pos.x = -3.12f;
			transform.position = pos;
		}
	}

	IEnumerator OnCollisionEnter2D(Collision2D col ){
		//____________________________________________________________________________________//death on collision with any of the sharp object
		if (col.gameObject.tag == "spikes" || col.gameObject.tag == "pencils") {
			GameController.PaddleMove = false;
			GameController.gameOver = true;
			Instantiate (particles, transform.position, Quaternion.identity);
			gameObject.GetComponent<Renderer> ().enabled = false;
			yield return new WaitForSeconds (1.5f);
			gameOverPlane.SetActive (true);
			RHCDPanel.SetActive (true);
			Destroy (gameObject);
			Time.timeScale = 0.00001f;

		}
		//____________________________________________________________________________________//clock collection 
		if (col.gameObject.tag == "clock") {
			Destroy (col.gameObject);
			Timer.timerCountdown += 15.0f;
		}
		//____________________________________________________________________________________//diamond collection
		if (col.gameObject.tag == "diamond") {
			DiamondCoin.diamondCount++;
			Destroy (col.gameObject);
		}

		if (col.gameObject.tag == "coins") {
			DiamondCoin.coinCount++;
			Destroy (col.gameObject);
		}
		yield return 0;
	}
}
