using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons.Editor
{

	[RequireComponent(typeof(Weapon))]
	public class ShowWeaponFirePoints : MonoBehaviour
	{

		Weapon script;

		private void OnDrawGizmosSelected() {
			if (!script)
				script = GetComponent<Weapon>();
			if (script.ConfigFile) {
				Gizmos.color = Color.red;
				Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(script.ConfigFile.LocalFirePoint), 0.2f);
			}
		}

	}
}