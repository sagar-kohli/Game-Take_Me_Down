using UnityEngine;
using System.Collections;

public class DestroyGameobject: MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y >= 4.0f)
			Destroy (gameObject);
	}
}
