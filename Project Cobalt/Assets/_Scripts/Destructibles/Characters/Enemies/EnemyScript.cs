using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : CharacterScript<EnemyConfig> {

	PlayerMechControllerScript playerScript;
	protected Transform target;
	bool awareOfPlayer = false;


	protected override void Initialisation() {
		base.Initialisation();
		playerScript = GameObject.Find("PlayerMech").GetComponent<PlayerMechControllerScript>();
	}

	protected bool AwareOfPlayer() {
		target = playerScript.transform;
		if (target) {
			if (awareOfPlayer)
				return true;
			else if ((target.position - transform.position).magnitude < configFile.AwarenessRadius)
				awareOfPlayer = true;
		}
		return false;
	}

}
