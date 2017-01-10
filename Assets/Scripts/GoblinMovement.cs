using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinMovement : MonoBehaviour {
	private Transform target;
	private NavMeshAgent agent;
	private Animator anim;
	private GoblinCombat cbt;

	private string playerTag = "Player";

	private float distance; // to target
	private Vector3 direction; // to target

	private bool isAggro = false; // is near target
	public float aggroRange = 60.0f; // acquire target range
	private float deaggroRange = 25.0f; // lose target range
	private float attackRange = 1.5f;

	private bool isRotate = false;
	private float rotationSpeed = 2.0f;

	private float walkSpeed = 2.0f;
	private float runSpeed = 3.5f;

	private Vector3 startPosition;
	private bool isReturn = false; // returning to initial position
	private float returnDistanceThreshold = 0.5f; // distance deviation allowed when returning to starting position

	private RaycastHit hit;
	private Vector3 rayOffset = new Vector3(0, 0.5f, 0);

	private bool isAttack = false; // is in attack animation
	private float attackDelay = 2.0f; // delay betewen attacks in seconds
	private float attackDelayTime; // time of end of last attack

	private bool isFirst = true; // is first attack cycle
	public bool aggroThroughObstacles = true;
	
	void Start () {
		target = GameObject.FindWithTag("Player").GetComponent<Transform>();
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		cbt = GetComponent<GoblinCombat>();

		startPosition = transform.position;
	}
	
	void Update () {
		if (!cbt.IsDead()) {
			distance = Vector3.Distance(transform.position, target.position);
			direction = (target.position - transform.position).normalized;

			HandleAnimationParams();

			if (!isAttack) {
				if (!isAggro) {
					CheckAggro();
				} else {
					AggressiveAction();
				}
			}
		}

	}

	private void FixedUpdate() {
		if (isRotate && !cbt.IsDead()) {
			// rotate to target
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
		}
	}

	private void AggressiveAction() {
		isReturn = false;

		if (!TargetInRange(deaggroRange) && !isFirst) {
			isAggro = false;
			isReturn = true;
			agent.SetDestination(startPosition);
			cbt.LeaveCombat();
		} else if (TargetInRange(attackRange)) {
			isFirst = false;
			AttackAction();		
			agent.Stop();
			isRotate = true;
		} else {
			agent.Resume();
			isRotate = false;
			agent.SetDestination(target.position);
		}
	}

	private void AttackAction() {
		if (!isAttack && Time.time - attackDelayTime > attackDelay) {
			anim.Play("ATTACK01", -1, 0f);
		}
	}

	public void SetAttack(bool value) {
		isAttack = value;
	}

	private void CheckAggro() {
		if (TargetInRange(aggroRange) && (aggroThroughObstacles || Physics.Raycast(transform.position + rayOffset, direction, out hit, aggroRange + 1))) {
			isAggro = aggroThroughObstacles || hit.transform.tag == playerTag;

			if (isAggro) {
				cbt.EnterCombat();
			}
		} else if (isReturn && Vector3.Distance(transform.position, startPosition) < returnDistanceThreshold) {
			agent.Stop();
			isFirst = true;
			isReturn = false;
		}
	}

	private bool TargetInRange(float range) {
		return distance < range;
	}

	// ANIMATION ...
	private void HandleAnimationParams() {
		if (agent.velocity.magnitude > 1.0f) {
			anim.SetBool("move", true);
		} else {
			anim.SetBool("move", false);
		}

		if (isReturn) {
			anim.SetBool("run", false);
			if (agent.speed == runSpeed) agent.speed = walkSpeed;

		} else {
			anim.SetBool("run", true);
			if (agent.speed == walkSpeed) agent.speed = runSpeed;
		}
	}

	public void AttackEvent() {
		cbt.HitTarget(distance < attackRange);
	}

	public void AttackStatus(int isPlaying) {
		isAttack = isPlaying == 0;

		// delay after end of animation
		if (!isAttack) {
			attackDelayTime = Time.time;
		}
	}

	public void HitEnd() {
		isAttack = false;
	}

	public void DeadEnd() {
		Destroy(gameObject);
	}
}
