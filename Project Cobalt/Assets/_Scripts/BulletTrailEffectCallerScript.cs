using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BulletTrailEffectCallerScript : MonoBehaviour
{

	void StopTrailEffect(Collision collision) {
		GetComponent<ParticleSystem>().Stop();
		transform.SetParent(null, true);
	}

	void OnEnable() {
		transform.parent.GetComponent<BulletScript>().OnImpact += StopTrailEffect;
	}

	void OnDisable() {
		if (transform.parent)
			transform.parent.GetComponent<BulletScript>().OnImpact -= StopTrailEffect;
	}

}
