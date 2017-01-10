using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinCombat : MonoBehaviour {

	private PlayerCombat player;
	private Animator anim;
	private GoblinMovement mov;
	private GameController ctrl;

	[SerializeField]
	private ParticleSystem particles;

	private int health = 100;

	private int idx; // index in player enemies list
	private bool isDead = false;
	
    public AudioSource[] sounds;
    public AudioSource GoblinNoise1;
    public AudioSource GoblinNoise2;
    public AudioSource GoblinDeath;
    public AudioSource GoblinHit;

	void Start () {
        sounds = GetComponents<AudioSource>();
        GoblinNoise1 = sounds[0];
        GoblinNoise2 = sounds[1];
        GoblinDeath = sounds[2];
        GoblinHit = sounds[3];
		
		player = GameObject.FindWithTag("Player").GetComponent<PlayerCombat>();
		anim = GetComponent<Animator>();
		mov = GetComponent<GoblinMovement>();
		ctrl = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}

	public void EnterCombat() {
		if(Random.value >= 0.5)
        {
            GoblinNoise1.Play();
        }
        else
        {
            GoblinNoise2.Play();
        }
		player.AddEnemy(this);
	}

	public void LeaveCombat() {
		player.RemoveEnemy(this);
	}

	public void OnHit(int value) {
		if (isDead) return;
		if (mov.GetIsAttack() && Random.value < 0.25) return; // miss chance

		anim.Play("HIT", -1, 0f);
		particles.Emit(Random.Range(10, 20));

		mov.SetAttack(true);


		health -= value;

		if (health <= 0) {
			HandleDeath();
		}
        else
        {
            GoblinHit.Play();
        }
	}

	public void HitTarget(bool isNear) {
		if (isNear) {
			player.OnHit(Random.Range(1, 7));
		}
	}

	private void HandleDeath() {
		LeaveCombat();
		isDead = true;
        GoblinDeath.Play();
		anim.Play("DIE", -1, 0f);
		ctrl.DecreaseGoblinCount();
		ctrl.SpawnGoblins(0, 2);
	}

	public bool IsDead() {
		return isDead;
	}
}
