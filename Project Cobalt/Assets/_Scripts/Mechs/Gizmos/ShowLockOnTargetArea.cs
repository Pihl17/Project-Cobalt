using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CombatMech))]
public class ShowLockOnTargetArea : MonoBehaviour
{
	CombatMech script;

	private void OnDrawGizmosSelected() {
		if (!script)
			script = GetComponent<CombatMech>();
		if (script.mechConfig) {
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(transform.position + transform.forward * (script.mechConfig.LockOnDistrance / 2 + 1f), Vector3.one * script.mechConfig.LockOnDistrance);
		}
	}
}
