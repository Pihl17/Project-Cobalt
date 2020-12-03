using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyScript))]
public class ShowEnemyAwarenessRadius : MonoBehaviour
{

	EnemyScript script;

	void OnDrawGizmosSelected() {
		if (!script)
			script = GetComponent<EnemyScript>();
		if (script.configFile) {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, script.configFile.AwarenessRadius);
		}
	}

}
