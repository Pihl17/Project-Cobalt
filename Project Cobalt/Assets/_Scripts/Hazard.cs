using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{

	public float damagePerSecond;

	public void OnTriggerStay(Collider other) {
		IDestructible target = other.GetComponent<IDestructible>();
		if (target != null) {
			target.Damage(damagePerSecond * Time.fixedDeltaTime);
		}
	}

}
