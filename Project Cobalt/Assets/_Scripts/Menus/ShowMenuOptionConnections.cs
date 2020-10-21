using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MenuOption))]
public class ShowMenuOptionConnections : MonoBehaviour
{
	MenuOption script;

	public void OnDrawGizmos() {
		script = GetComponent<MenuOption>();
		if (script) {
			Gizmos.color = Color.red;
			if (script.nearbyOption.right) {
				Vector3 topPoint = script.nearbyOption.right.transform.position + Vector3.up*10 + Vector3.left*10;
				Vector3 tip = script.nearbyOption.right.transform.position + Vector3.left*5;
				Vector3 botPoint = script.nearbyOption.right.transform.position + Vector3.down*10 + Vector3.left*10;
				Gizmos.DrawLine(transform.position, topPoint);
				Gizmos.DrawLine(topPoint,tip);
				Gizmos.DrawLine(tip,botPoint);
				Gizmos.DrawLine(botPoint,transform.position);
			}
			if (script.nearbyOption.left) {
				Vector3 topPoint = script.nearbyOption.left.transform.position + Vector3.up*10 + Vector3.right*10;
				Vector3 tip = script.nearbyOption.left.transform.position + Vector3.right*5;
				Vector3 botPoint = script.nearbyOption.left.transform.position + Vector3.down*10 + Vector3.right*10;
				Gizmos.DrawLine(transform.position, topPoint);
				Gizmos.DrawLine(topPoint,tip);
				Gizmos.DrawLine(tip,botPoint);
				Gizmos.DrawLine(botPoint,transform.position);
			}
		}
	}

}
