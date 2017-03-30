using UnityEngine;
using System.Collections;

public class bottomPencilMovement : MonoBehaviour {

	float upperY = -6.75f;												
	float lowerY = -4.6f;												

	
	void FixedUpdate () {
		if (GameController.PaddleMove == true) {
			float t = (Score.score)/1000;
			Vector3 temp = transform.position;
			temp.y = Mathf.Lerp(upperY,lowerY,t);
			transform.position = temp;
		}
	}
}
