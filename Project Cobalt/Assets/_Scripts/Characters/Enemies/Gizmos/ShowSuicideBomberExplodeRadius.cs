using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowSuicideBomberExplodeRadius : MonoBehaviour
{

	EnemyScript script;

	void OnDrawGizmos() {
		if (!script)
			script = GetComponent<EnemyScript>();
		if (script.configFile) {
			Gizmos.color = new Color(1, 0.75f, 0);
			Gizmos.DrawWireSphere(transform.position, script.configFile.AttackRange);
		}
	}

}
