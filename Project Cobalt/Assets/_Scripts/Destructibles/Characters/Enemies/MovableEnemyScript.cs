using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class MovableEnemyScript : EnemyScript
{

	protected UnityEngine.AI.NavMeshAgent agent;

	protected override void Initialisation() {
		base.Initialisation();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.speed = configFile.MoveSpeed;
	}

	void Update() {
		if (AwareOfPlayer())
			Move();
	}

	protected virtual void Move() {
		if (target)
			agent.SetDestination(target.position);
	}

}
