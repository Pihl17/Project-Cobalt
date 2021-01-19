using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{

	protected abstract void PickUpEffect(CombatMech mech);

	void OnTriggerEnter(Collider other) {
		if (other.GetComponent<CombatMech>()) {
			PickUpEffect(other.GetComponent<CombatMech>());
			Destroy(gameObject);
		}
	}

}
