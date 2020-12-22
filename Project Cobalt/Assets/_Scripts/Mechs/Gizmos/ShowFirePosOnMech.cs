using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CombatMech))]
public class ShowFirePosOnMech : MonoBehaviour
{
	CombatMech script;

	private void OnDrawGizmos() {
		if (!script)
			script = GetComponent<CombatMech>();
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(script.mechConfig.GunLocation), 0.1f);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(script.mechConfig.ArtilleryLocation), 0.5f);
	}
}
