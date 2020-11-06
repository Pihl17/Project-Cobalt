using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RobotAbilityUseScript))]
public class ShowFireLocationsOnPlayer : MonoBehaviour
{

	RobotAbilityUseScript script;

	private void OnDrawGizmos() {
		if (!script)
			script = GetComponent<RobotAbilityUseScript>();
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position + script.GunLocation, 0.1f);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position + script.LauncherLocation, 0.5f);
	}

}
