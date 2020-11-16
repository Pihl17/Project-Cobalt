using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyEffectScript : MonoBehaviour
{
	
	void StartEffect() {
		GetComponent<ParticleSystem>().Play();
		transform.SetParent(null, true);
	}

	void OnEnable() {
		if (transform.parent && transform.parent.GetComponent<IDestructible>() != null)
			transform.parent.GetComponent<IDestructible>().OnDestroy += StartEffect;
	}



	void OnDisable() {
		if (transform.parent && transform.parent.GetComponent<IDestructible>() != null)
			transform.parent.GetComponent<IDestructible>().OnDestroy -= StartEffect;
	}
}
