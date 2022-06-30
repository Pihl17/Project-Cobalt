using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CombatMech))]
public class ShowMechWeaponPlacements : MonoBehaviour
{
	CombatMech script;

	private void OnDrawGizmos() {
		if (!script)
			script = GetComponent<CombatMech>();
		if (script.mechConfig) {
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(transform.position + transform.TransformDirection(script.mechConfig.GunLocation), Vector3.one * 0.1f);
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(transform.position + transform.TransformDirection(script.mechConfig.HeavyLocation), Vector3.one * 0.2f);
			Gizmos.color = Color.blue;
			Gizmos.DrawWireCube(transform.position + transform.TransformDirection(script.mechConfig.ArtilleryLocation), Vector3.one * 0.5f);
		}
	}
}
