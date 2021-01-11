using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MenuOption))]
public class ShowMenuOptionConnections : MonoBehaviour
{
	MenuOption script;

	public void OnDrawGizmosSelected() {
		script = GetComponent<MenuOption>();
		if (script) {
			Gizmos.color = Color.green;
			for (int i = 0; i < script.nearbourOption.Length; i++) {
				if (script.nearbourOption[i]) {
					//Gizmos.DrawLine(transform.position, script.nearbourOption[i].transform.position);
					Gizmos.DrawIcon(script.nearbourOption[i].transform.position, ((MenuOption.Direction)i).ToString() + "Arrow", false, Color.green);
				}
			}
		}
	}

}
