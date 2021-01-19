using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ProjectileImpactEffectCaller : MonoBehaviour
{

	void ImpactEffect(Collision collision) {
		GetComponent<ParticleSystem>().Play();
		transform.SetParent(null, true);
	}

	void OnEnable() {
		transform.parent.GetComponent<Projectile>().OnImpact += ImpactEffect;
	}

	void OnDisable() {
		if (transform.parent)
			transform.parent.GetComponent<Projectile>().OnImpact -= ImpactEffect;
	}

}
