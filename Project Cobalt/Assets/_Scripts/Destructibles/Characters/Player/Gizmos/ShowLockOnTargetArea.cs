using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MechSystemScript))]
public class ShowLockOnTargetArea : MonoBehaviour
{
	MechSystemScript script;

	private void OnDrawGizmosSelected() {
		if (!script)
			script = GetComponent<MechSystemScript>();
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position + transform.forward * (script.configFile.LockOnDistrance / 2 + 1f), Vector3.one * script.configFile.LockOnDistrance);
	}
}
