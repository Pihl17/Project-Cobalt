using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public abstract class WalkableEnemyScript : EnemyScript
{

	protected UnityEngine.AI.NavMeshAgent agent;

	protected override void Initialization() {
		base.Initialization();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

		// Start is called before the first frame update
		void Start()
    {
		Initialization();
	}

    // Update is called once per frame
    void Update()
    {
		if (AwareOfPlayer())
			Move();
	}

	protected abstract void Move();


	


}
