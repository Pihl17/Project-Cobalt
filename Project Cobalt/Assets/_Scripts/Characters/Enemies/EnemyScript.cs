using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : DestructableScript
{
	public PlayerControlScript playerScript;
	protected Transform target;
	bool awareOfPlayer = false;
	public float awarenessRadius = 10;

	protected override void Initialization() {
		base.Initialization();
		playerScript = GameObject.Find("PlayerController").GetComponent<PlayerControlScript>();
	}

	protected bool AwareOfPlayer() {
		target = playerScript.currentRobot.transform;
		if (target) {
			if (awareOfPlayer)
				return true;
			else if ((target.position - transform.position).magnitude < awarenessRadius)
				awareOfPlayer = true;
		}
		return false;
	}


	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, awarenessRadius);
	}

}
