using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ProjectileTrailEffectCaller : MonoBehaviour
{

	void StopTrailEffect(Collision collision) {
		GetComponent<ParticleSystem>().Stop();
		transform.SetParent(null, true);
	}

	void OnEnable() {
		transform.parent.GetComponent<Projectile>().OnImpact += StopTrailEffect;
	}

	void OnDisable() {
		if (transform.parent)
			transform.parent.GetComponent<Projectile>().OnImpact -= StopTrailEffect;
	}

}
