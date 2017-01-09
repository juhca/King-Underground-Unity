using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Transform cam;
	private Rigidbody rb;
	private Animator anim;

	private PlayerCombat cbt;

	private Vector3 camForward; // direction of camera
	private Vector3 move; // vector of movement
	private float h, v; // keyboard direction input
	private float mousex, mousey; // mouse input

	public float speed = 7f;
	private Vector3 downVelocity = new Vector3(0, -2f, 0);

	void Start () {
		if (Camera.main != null) {
			cam = Camera.main.transform;
		} else {
			Debug.LogError("Character camera not found.");
		}

		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;

		anim = GetComponent<Animator>();

		Cursor.lockState = CursorLockMode.Locked;

		cbt = GetComponent<PlayerCombat>();
	}
	
	void Update () {

		if (Input.GetMouseButtonDown(0) && !cbt.IsDead()) {
			int layer = IsMoving() ? 1 : 0;

			string animationName = "SWORD0" + (int) Random.Range(1, 3);
			anim.Play(animationName, layer, 0f);
		}

	}

	private void FixedUpdate() {
		if (!cbt.IsDead()) {
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");

			anim.SetFloat("inputH", h);
			anim.SetFloat("inputV", v);

			if (cam != null) {
				camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;

				move = speed * v * camForward + speed * h * cam.right;

				if (IsMoving()) {
					transform.rotation = Quaternion.LookRotation(camForward);
				}

				rb.velocity = downVelocity + move;
			}
		}
	}

	private bool IsMoving() {
		return h < -0.1 || h > 0.1 || v < -0.1 || v > 0.1;
	}
}
