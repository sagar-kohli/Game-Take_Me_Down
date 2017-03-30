using UnityEngine;
using System.Collections;

public class topPencilMove : MonoBehaviour {

	float upperY = 6f;		
	float lowerY = 4.0f;


	void FixedUpdate () {
		if (GameController.PaddleMove == false)
			return;
		Vector3 temp = transform.position;
		temp.y = Mathf.Lerp (upperY, lowerY, (Score.score) / 1000);
		transform.position = temp;

	}
}
