using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class WalkableEnemyScript : EnemyScript
{

	UnityEngine.AI.NavMeshAgent agent;

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
			agent.destination = target.position;
    }


	public void OnCollisionEnter(Collision col) {
		if (col.gameObject.GetComponent<RobotScript>()) {
			col.gameObject.GetComponent<RobotScript>().Damage(1);
			GameObject.Destroy(gameObject);
		}
	}


}
