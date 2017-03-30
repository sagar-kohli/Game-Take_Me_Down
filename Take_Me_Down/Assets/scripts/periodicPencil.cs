using UnityEngine;
using System.Collections;

public class periodicPencil : MonoBehaviour {
	float x = 7.0f;
		
	void FixedUpdate () {
		if (GameController.PaddleMove == false)
			return;
		float presentTime = Time.time;
		Vector3 temp = transform.position;
		if (temp.x >= 0)
			temp.y += 0.025f *  Mathf.Sin (x * (presentTime) + temp.x);
		else
			temp.y += 0.025f *  Mathf.Sin (x * (presentTime) - temp.x);
		transform.position = temp;
	}
}