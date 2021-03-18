using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerDetector : MonoBehaviour
{

	public GameObject TheDetectingUnit;
	IDetectingUnit DetectingUnit;

	void Start() {
		GetComponent<SphereCollider>().radius = DetectingUnit.GetWeaponConfig().Range;
	}

	private void OnTriggerEnter(Collider other) {
		PlayerControl target = other.GetComponent<PlayerControl>();
		if (target) {
			DetectingUnit.AddTarget(target.transform);
		}
	}

	private void OnTriggerExit(Collider other) {
		PlayerControl target = other.GetComponent<PlayerControl>();
		if (target) {
			DetectingUnit.RemoveTarget(target.transform);
		}
	}

	private void OnValidate() {
		if (TheDetectingUnit) {
			DetectingUnit = TheDetectingUnit.GetComponent<IDetectingUnit>();
			if (DetectingUnit == null) TheDetectingUnit = null;
		}
	}

}
