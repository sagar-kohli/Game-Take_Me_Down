using UnityEngine;
using System.Collections;

public class PaddleScript : MonoBehaviour {

	public float paddleSpeed =2.5f;

	public static float initiationFactor = 0.0f;		//linear increase (slow movement of paddle in beggining) value of speed

	float xvaluemin = -2.35f;				//screen max min value in space
	float xvaluemax = 2.35f;

	public GameObject preclock;
	public GameObject diamond;
	public GameObject coin;

	public static float spikeNum = 0;


	void Start(){

	}

	void Update () {
		//*****************************************************************************//distruction and initiation of paddles 
		if (transform.position.y > 4.5) {

			if (gameObject.tag == "spikes")
				spikeNum++;
			//Debug.Log ("count:" + spikeNum);
			int ranDiom = Random.Range(0,200);				//diamond random 
			int ranCoin = Random.Range(0,10);				//coin random

			Vector3 pos = Vector3.zero;
			pos.x = Random.Range (xvaluemin, xvaluemax);
			pos.y = transform.position.y - 9;
			pos.z = -1f;

			Destroy (gameObject);

			if (spikeNum >= 10) {
				//Debug.Log ("nhi aaya");
				PaddleWithObject (preclock, pos);
				spikeNum = 0;
			} 
			else if (ranCoin == 5) {
				PaddleWithObject (coin, pos);
			}
			else if (ranDiom == 42){
				PaddleWithObject (diamond, pos);
			}
			else {
				int prefabIndex = Random.Range (0, 4);
				Instantiate (RandomPaddles.prefabList [prefabIndex], pos, Quaternion.identity);
			}
		}
	}

	void FixedUpdate () {  
		//*****************************************************************************//paddle movement low to fast initially
		if (GameController.PaddleMove) {
			float speed = paddleSpeed;
			if (initiationFactor < 1) {
				initiationFactor += Time.deltaTime * .2f;
				speed = Mathf.Lerp(0, paddleSpeed, initiationFactor);
			}
			transform.Translate (0, speed*Time.deltaTime, 0);
		}
	}

	void PaddleWithObject (GameObject gmobj, Vector3 position){
		int prefabIndex = Random.Range (0, 3);
		Instantiate (RandomPaddles.prefabList [prefabIndex], position, Quaternion.identity);
		Instantiate (gmobj, position + new Vector3 (0, 0.75f, 0), Quaternion.identity);
	}
}
