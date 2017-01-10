using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour {

	public Transform target;

	private GameController ctrl;

	private float distance;

	private float initialY; // height at start
	private float endY; // height at end

	private float nearRange = 2.0f;
	private float leverSpeed = 0.1f;

	private bool isFinished = false;
	
	void Start () {
		ctrl = GameObject.FindWithTag("GameController").GetComponent<GameController>();

		initialY = transform.position.y;
		endY = initialY - 0.31f; // height of hole
	}
	
	void Update () {
		if (!isFinished) {
			distance = Vector3.Distance(transform.position, target.position);

			if (distance < nearRange) {
				if (Input.GetKey(KeyCode.F)) {
					LeverDown();
				}
			}
		}
	}

	private void LeverDown() {
		transform.Translate(0, 0, -leverSpeed * Time.deltaTime);
		if (transform.position.y < endY) {
			isFinished = true;
			ctrl.LeverDown();
			ctrl.SpawnGoblins(4, 6);
		}
	}
}
