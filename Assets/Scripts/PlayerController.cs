using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	
		// tabela zvokov
    public AudioSource[] sounds;
    public AudioSource SwordSlash1;
    public AudioSource SwordSlash2;
    public AudioSource ManGrunt1;
    public AudioSource ManGrunt2;
    public AudioSource Footsteps;

	void Start () {
		if (Camera.main != null) {
			cam = Camera.main.transform;
            sounds = GetComponents<AudioSource>();
            SwordSlash1 = sounds[0];
            SwordSlash2 = sounds[1];
            ManGrunt1 = sounds[2];
            ManGrunt2 = sounds[3];
            Footsteps = sounds[4];
		} else {
			Debug.LogError("Character camera not found.");
		}

		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;

		anim = GetComponent<Animator>();

		cbt = GetComponent<PlayerCombat>();
	}
	
	void Update () {

		// Attack
		if (Input.GetMouseButtonDown(0) && !cbt.IsDead() && Time.timeScale == 1) {
			//transform.rotation = Quaternion.LookRotation(camForward);

			int layer = IsMoving() ? 1 : 0;
            string animationName = "SWORD0" + (int) Random.Range(1, 3);
            if(animationName == "SWORD01")
            {
                SwordSlash2.Play();
                ManGrunt2.Play();
            }
            else if(animationName == "SWORD02")
            {
                SwordSlash1.Play();
                ManGrunt1.Play();
            }
            else
            {
                Debug.Log(animationName);
            }
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
					if(!Footsteps.isPlaying)
					{
						Footsteps.Play();
					}
					transform.rotation = Quaternion.LookRotation(camForward);
				}

				rb.velocity = downVelocity + move;
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Finish") {
			Cursor.lockState = CursorLockMode.None;
			SceneManager.LoadScene("win");
		}
	}

	private bool IsMoving() {
		return h < -0.1 || h > 0.1 || v < -0.1 || v > 0.1;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "fire_crystal3")
        {
            var h = GetComponent<PlayerCombat>().returnHealth();
            if(h != 100)
            {
                GetComponent<PlayerCombat>().increaseHealth();
                collision.gameObject.SetActive(false);
            }
        }
    }
}
