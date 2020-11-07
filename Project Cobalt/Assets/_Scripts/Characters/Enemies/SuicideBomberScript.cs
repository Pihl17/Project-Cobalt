using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomberScript : WalkableEnemyScript {


	protected override void Initialization() {
		base.Initialization();
		OnDestroy += Explode;
	}

	// Start is called before the first frame update
	void Start()
    {
		Initialization();
	}

    // Update is called once per frame
    void Update()
    {
		if (AwareOfPlayer()) {
			Move();
		}
	}

	protected override void Move() {
		agent.SetDestination(target.position);
	}

	void Explode() {
		OnDestroy -= Explode;
		Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, configFile.AttackRange);
		for (int i = 0; i < nearbyObjects.Length; i++) {
			if (nearbyObjects[i].GetComponent<RobotScript>())
				nearbyObjects[i].GetComponent<RobotScript>().Damage(configFile.Damage);
			else if (nearbyObjects[i].GetComponent<DestructableScript>())
				nearbyObjects[i].GetComponent<DestructableScript>().Damage(configFile.Damage);
		}
	}

	public void OnCollisionEnter(Collision col) {
		if (col.gameObject.GetComponent<RobotScript>()) {
			//col.gameObject.GetComponent<RobotScript>().Damage(1);
			Die();
		}
	}

}
