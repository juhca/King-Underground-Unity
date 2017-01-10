using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour {

	private GameObject[] pauseObjects;

	void Start () {
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		HidePaused();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			PauseControl();
		}
	}

	private void PauseControl() {
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
			ShowPaused();
		} else if (Time.timeScale == 0) {
			Time.timeScale = 1;
			HidePaused();
		}
	}

	public void HidePaused() {
		foreach (GameObject g in pauseObjects) {
			g.SetActive(false);
		}

		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1;
	}

	public void ShowPaused() {
		foreach (GameObject g in pauseObjects) {
			g.SetActive(true);
		}

		Cursor.lockState = CursorLockMode.Confined;
		Time.timeScale = 0;
	}

	public void ToMenu() {
		SceneManager.LoadScene("menu");
	}
}
