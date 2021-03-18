using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerDetector : MonoBehaviour
{

	public Turret detectingUnit;

	void Start() {
		GetComponent<SphereCollider>().radius = detectingUnit.GetWeaponConfig().Range;
	}

	private void OnTriggerEnter(Collider other) {
		PlayerControl target = other.GetComponent<PlayerControl>();
		if (target) {
			detectingUnit.AddTarget(target);
		}
	}

	private void OnTriggerExit(Collider other) {
		PlayerControl target = other.GetComponent<PlayerControl>();
		if (target) {
			detectingUnit.RemoveTarget(target);
		}
	}

}
