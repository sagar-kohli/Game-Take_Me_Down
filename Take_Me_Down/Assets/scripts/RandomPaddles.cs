using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomPaddles : MonoBehaviour {

	float xvaluemin = -2.35f;				//screen max min value in space
	float xvaluemax = 2.35f;

	public static List<GameObject> prefabList = new List<GameObject>();
	public GameObject Prefab1;
	public GameObject Prefab2;
	public GameObject Prefab3;
	public GameObject prefab4;

	void Awake(){
		prefabList.Add(Prefab1);
		prefabList.Add(Prefab2);
		prefabList.Add(Prefab3);
		prefabList.Add (prefab4);
		for (int i = 0; i < 12; i++) {
			int x = 3;
			if (i > 7)
				x = 4;
			int prefabIndex = UnityEngine.Random.Range (0, x);
			Vector3 pos = Vector3.zero;
			if (i == 5)
				pos.x = 0f;
			else
				pos.x = Random.Range (xvaluemin, xvaluemax);
			pos.y = 3 - i * 0.75f;
			pos.z = -1;
			Instantiate (prefabList [prefabIndex],pos , Quaternion.identity);
		}
	}




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
