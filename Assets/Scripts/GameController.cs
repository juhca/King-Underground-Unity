using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject goblinPrefab;

	public Transform door;

	public int leverCount = 3;

	private float doorEndY; // y of door at end
	private float doorSpeed = 0.2f;
	private Vector3[] spawnPositions = new Vector3[4];

	private int activeGoblins = 6; // stevilo goblinov v sceni
	private int maxGoblins = 15;

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;

		doorEndY = door.position.y - 3.13f;

		spawnPositions[0] = new Vector3(-50f, 2.76f, 23f);
		spawnPositions[1] = new Vector3(-48f, 2.5f, 47f);
		spawnPositions[2] = new Vector3(-100f, 2.62f, 47f);
		spawnPositions[3] = new Vector3(-100f, 2.48f, 25.4f);
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)) {
			GameObject clone = Instantiate(goblinPrefab, spawnPositions[Random.Range(0, spawnPositions.Length)], Quaternion.identity);
		}

		// move door
		if (leverCount <= 0 && door.position.y > doorEndY) {
			door.Translate(0, 0, -doorSpeed * Time.deltaTime);
		}
	}

	public void LeverDown() {
		leverCount--;
	}

	public void DecreaseGoblinCount() {
		activeGoblins--;
	}

	public void SpawnGoblins(int min, int max) {
		if (activeGoblins >= maxGoblins) return;
		int num = Random.Range(min, max);
		for (; num > 0; num--) {
			if (activeGoblins >= maxGoblins) return;
			Instantiate(goblinPrefab, spawnPositions[Random.Range(0, spawnPositions.Length)], Quaternion.identity);
			activeGoblins++;
		}
	}
}
