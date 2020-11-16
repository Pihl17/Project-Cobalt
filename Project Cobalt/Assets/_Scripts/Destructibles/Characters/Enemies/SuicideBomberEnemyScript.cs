using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomberEnemyScript : MovableEnemyScript
{

	protected override void Initialisation() {
		base.Initialisation();
		OnDestroy += Explode;
	}

	void Update() {
		if (AwareOfPlayer())
			Move();
	}

	protected override void Move() {
		if (target)
			agent.SetDestination(target.position);
	}

	void Explode() {
		OnDestroy -= Explode;
		Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, configFile.AttackRange);
		for (int i = 0; i < nearbyObjects.Length; i++) {
			if (nearbyObjects[i].GetComponent<DestructibleScript<DestructibleConfig>>())
				nearbyObjects[i].GetComponent<DestructibleScript<DestructibleConfig>>().Damage(configFile.Damage);
		}
	}

	public void OnCollisionEnter(Collision col) {
		if (col.gameObject.GetComponent<PlayerMechControllerScript>()) {
			Die();
		}
	}


}
