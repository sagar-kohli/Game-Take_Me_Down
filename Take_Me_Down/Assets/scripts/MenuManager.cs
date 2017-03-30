using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	/// <summary>
	/// Main Menu Controller.
	/// This class handles user clicks on menu button, and also fetch and shows user saved scores on screen.
	/// </summary>
	
	private bool canTap;						//are we allowed to click on buttons? (prevents double touch)
	private float buttonAnimationSpeed = 9.0f;	//button scale animation speed

	public GameObject diamondText;
	public GameObject coinsText;

	int savedDiamond =0;
	int savedCoins = 0;

	void Awake () {
		
		canTap = true; //player can tap on buttons
	}


	void Start() {
		//prevent screenDim in handheld devices
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		savedDiamond = PlayerPrefs.GetInt ("diamonds", 0);
		savedCoins = PlayerPrefs.GetInt ("coins", 0);

		diamondText.GetComponent<TextMesh> ().text = savedDiamond.ToString ();
		coinsText.GetComponent<TextMesh> ().text = savedCoins.ToString ();
	}


	void Update () {
		if(canTap)	
			StartCoroutine(tapManager());
	}
	
	
	///***********************************************************************
	/// Process user inputs
	///***********************************************************************
	private RaycastHit hitInfo;
	private Ray ray;
	IEnumerator tapManager() {
		
		//Mouse of touch?
		if(	Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Ended)  
			ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
		else if(Input.GetMouseButtonUp(0))
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		else
			yield break;
		
		if (Physics.Raycast(ray, out hitInfo)) {
			GameObject objectHit = hitInfo.transform.gameObject;
			switch(objectHit.name) {
				case "Play":
					canTap = false;
					StartCoroutine (animateButton (objectHit));
					yield return new WaitForSeconds (.50f);
					SceneManager.LoadScene (1);
					break;

				case "Options":
					canTap = false;
					StartCoroutine(animateButton(objectHit));
					yield return new WaitForSeconds(.50f);
					//to do something
					break;
					
				case "GooglePlay":
					canTap = false;
					StartCoroutine(animateButton(objectHit));
					yield return new WaitForSeconds(.50f);
					//to so something
					break;
			}	
		}
	}
	

	///***********************************************************************
	/// Animate button by modifying it's scales
	///***********************************************************************
	IEnumerator animateButton(GameObject _btn) {

		Vector3 startingScale = _btn.transform.localScale;
		Vector3 destinationScale = startingScale * 1.1f;
		float t = 0.0f; 
		while (t <= 1.0f) {
			t += Time.deltaTime * buttonAnimationSpeed;
			_btn.transform.localScale = new Vector3(Mathf.SmoothStep(startingScale.x, destinationScale.x, t),
				Mathf.SmoothStep(startingScale.y, destinationScale.y, t),
				_btn.transform.localScale.z);
			yield return 0;
		}
		
		float r = 0.0f; 
		if(_btn.transform.localScale.x >= destinationScale.x) {
			while (r <= 1.0f) {
				r += Time.deltaTime * buttonAnimationSpeed;
				_btn.transform.localScale = new Vector3(Mathf.SmoothStep(destinationScale.x, startingScale.x, r),
					Mathf.SmoothStep(destinationScale.y, startingScale.y, r),
					_btn.transform.localScale.z);
				yield return 0;
			}
		}
		
		if (r >= 1)
			yield return new WaitForSeconds (.5f);
			canTap = true;
	}
}
