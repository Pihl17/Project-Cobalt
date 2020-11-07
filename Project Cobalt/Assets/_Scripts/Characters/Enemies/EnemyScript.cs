using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : DestructableScript
{
	PlayerControlScript playerScript;
	protected Transform target;
	bool awareOfPlayer = false;
	

	protected override void Initialization() {
		base.Initialization();
		playerScript = GameObject.Find("PlayerController").GetComponent<PlayerControlScript>();
	}

	protected bool AwareOfPlayer() {
		target = playerScript.currentRobot.transform;
		if (target) {
			if (awareOfPlayer)
				return true;
			else if ((target.position - transform.position).magnitude < configFile.AwarenessRadius)
				awareOfPlayer = true;
		}
		return false;
	}


	

}
