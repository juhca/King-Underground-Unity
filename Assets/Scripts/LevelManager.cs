using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public Transform mainMenu, instructionsMenu, storyMenu;

	public void LoadScene(string name) {
		SceneManager.LoadScene(name);
	}

	public void QuitGame() {
		Application.Quit();
	}

	public void InstructionsMenu(bool clicked) {
		if(clicked == true) {
			instructionsMenu.gameObject.SetActive(clicked);
			mainMenu.gameObject.SetActive(false);
		}
		else {
			instructionsMenu.gameObject.SetActive(clicked);
			mainMenu.gameObject.SetActive(true);
		}
	}

	public void StoryMenu(bool clicked) {
		if(clicked == true) {
			storyMenu.gameObject.SetActive(clicked);
			mainMenu.gameObject.SetActive(false);
		}
		else {
			storyMenu.gameObject.SetActive(clicked);
			mainMenu.gameObject.SetActive(true);
		}
	}
}
