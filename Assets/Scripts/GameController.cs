using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject goblinPrefab;

	public Transform door;

	public int leverCount = 3;

	private float doorEndY; // y of door at end
	private float doorSpeed = 0.2f;

	void Start () {
		doorEndY = door.position.y - 3.13f;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)) {
			GameObject clone = Instantiate(goblinPrefab, new Vector3(-74f, 3f, 48f), Quaternion.identity);
		}

		// move door
		if (leverCount <= 0 && door.position.y > doorEndY) {
			door.Translate(0, 0, -doorSpeed * Time.deltaTime);
		}
	}

	public void LeverDown() {
		leverCount--;
	}
}
