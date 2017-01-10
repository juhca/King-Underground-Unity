using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour {

	private int health = 100;
	public Text countText;

	private List<GoblinCombat> enemies = new List<GoblinCombat>();

	private Animator anim;

	private float attackRange = 2.0f;
	private float attackAngle = 85f;

	private bool isDead = false;
	
	void Start () {
		anim = GetComponent<Animator>();
		SetCountText();
	}

	public void AddEnemy(GoblinCombat g) {
		enemies.Add(g);
	}

	public void RemoveEnemy(GoblinCombat g) {
		enemies.Remove(g);
	}

	public void OnHit(int value) {
		if (isDead) return;
		
		anim.Play("HIT", -1, 0f);

		health -= value;
		SetCountText();

		if (health <= 0) {
			HandleDeath();
		}
	}

	public void HitTargets() {
		foreach (GoblinCombat g in enemies.ToArray()) {
			if (Vector3.Distance(transform.position, g.transform.position) < attackRange && Vector3.Dot((g.transform.position - transform.position).normalized, transform.forward) > Mathf.Cos(attackAngle)) {
				g.OnHit(15);
			}
		}
	}

	private void HandleDeath() {
		isDead = true;
		anim.Play("DIE", -1, 0f);
	}

	public bool IsDead() {
		return isDead;
	}

	void SetCountText() {
		countText.text = health.ToString();
	}

    public void increaseHealth()
    {
        if (health <= 90) health += 10;
        else health = 100;
        countText.text = health.ToString();
    }

    public int returnHealth()
    {
        return health;
    }
}
