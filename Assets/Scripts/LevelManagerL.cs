using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerL : MonoBehaviour {

	public void LoadScene() {
		SceneManager.LoadScene("scene01");
	}

	public void ToMenu() {
		SceneManager.LoadScene("menu");
		// SceneManager.SetActiveScene(SceneManager.GetSceneByName("menu"));
	}
}
