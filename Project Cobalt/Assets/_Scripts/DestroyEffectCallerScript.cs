using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyEffectCallerScript : MonoBehaviour
{

	void StartEffect() {
		GetComponent<ParticleSystem>().Play();
		transform.SetParent(null, true);
	}

	void OnEnable() {
		transform.parent.GetComponent<DestructableScript>().OnDestroy += StartEffect;
	}

	void OnDisable() {
		if (transform.parent)
			transform.parent.GetComponent<DestructableScript>().OnDestroy -= StartEffect;
	}
}
