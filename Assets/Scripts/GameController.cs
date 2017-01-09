using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject goblinPrefab;

	void Start () {
		
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)) {
			GameObject clone = Instantiate(goblinPrefab, new Vector3(-74f, 3f, 48f), Quaternion.identity);
		}
	}
}
