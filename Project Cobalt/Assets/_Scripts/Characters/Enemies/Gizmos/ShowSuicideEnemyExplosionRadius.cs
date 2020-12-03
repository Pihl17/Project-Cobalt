using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SuicideBomberEnemyScript))]
public class ShowSuicideEnemyExplosionRadius : MonoBehaviour
{

	SuicideBomberEnemyScript script;

	void OnDrawGizmos() {
		if (!script)
			script = GetComponent<SuicideBomberEnemyScript>();
		if (script.configFile) {
			Gizmos.color = new Color(1, 0.75f, 0);
			Gizmos.DrawWireSphere(transform.position, script.configFile.AttackRange);
		}
	}

}
