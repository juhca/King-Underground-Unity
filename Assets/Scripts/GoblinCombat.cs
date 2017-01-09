using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinCombat : MonoBehaviour {

	private PlayerCombat player;
	private Animator anim;
	private GoblinMovement mov;

	private int health = 100;

	private int idx; // index in player enemies list
	private bool isDead = false;

	void Start () {
		player = GameObject.FindWithTag("Player").GetComponent<PlayerCombat>();
		anim = GetComponent<Animator>();
		mov = GetComponent<GoblinMovement>();
	}

	public void EnterCombat() {
		player.AddEnemy(this);
	}

	public void LeaveCombat() {
		player.RemoveEnemy(this);
	}

	public void OnHit(int value) {
		if (isDead) return;

		anim.Play("HIT", -1, 0f);
		mov.SetAttack(true);

		health -= value;

		if (health < 0) {
			HandleDeath();
		}
	}

	public void HitTarget(bool isNear) {
		if (isNear) {
			player.OnHit(5);
		}
	}

	private void HandleDeath() {
		LeaveCombat();
		isDead = true;
		anim.Play("DIE", -1, 0f);
	}

	public bool IsDead() {
		return isDead;
	}
}
