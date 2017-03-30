using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour {

	private bool canTap;						//are we allowed to click on buttons? (prevents double touch)
	private float buttonAnimationSpeed = 900000.0f;	//button scale animation speed
	public GameObject pausePanel ;
	public GameObject RHCDPanel;

	void Awake () {

		canTap = true; //player can tap on buttons
		pausePanel.SetActive (false);
		RHCDPanel.SetActive (false);
	}

	void Start() {
		//prevent screenDim in handheld devices
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
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
			//Debug.Log (objectHit.name);
			switch(objectHit.name) {
			case "pauseButton":
				if (GameController.PaddleMove == true) {
					GameController.PaddleMove = false;
					StartCoroutine (animateButton (objectHit));
					yield return new WaitForSeconds (.0000050f);
					Time.timeScale = 0.00001f;
					pausePanel.SetActive (true);
					RHCDPanel.SetActive (true);
				}
					break;

			case "resume":
				canTap = false;
				//playSfx(menuTap);
				//PlayerPrefs.SetInt("GameMode", 1);
				GameController.PaddleMove = true;
				StartCoroutine(animateButton(objectHit));
				yield return new WaitForSeconds(.0000050f);					
				//Application.LoadLevel("Game");
				pausePanel.SetActive (false);
				RHCDPanel.SetActive (false);
				Time.timeScale = 1;
				break;

			case "RestartButton":
				canTap = false;
				//playSfx(menuTap);
				//PlayerPrefs.SetInt("GameMode", 1);
				StartCoroutine (animateButton (objectHit));
				yield return new WaitForSeconds (.0000050f);					
				SceneManager.LoadScene (1);
				pausePanel.SetActive (false);
				RHCDPanel.SetActive (false);
				Time.timeScale = 1;
				break;

			case "homeButton":
				canTap = false;
				//playSfx(menuTap);
				//PlayerPrefs.SetInt("GameMode", 1);
				StartCoroutine (animateButton (objectHit));
				yield return new WaitForSeconds (.0000050f);					
				SceneManager.LoadScene (0);
				pausePanel.SetActive (false);
				RHCDPanel.SetActive (false);
				Time.timeScale = 1;
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

		if(r >= 1)
			canTap = true;
	}
}