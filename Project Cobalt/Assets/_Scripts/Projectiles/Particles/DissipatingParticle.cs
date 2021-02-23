using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ParticleSystem))]
public class DissipatingParticle : MonoBehaviour
{

	SphereCollider col;
	Rigidbody rig;
	ParticleSystem particleSys;
	public float dissipationRate = 1f;
	public float maxDissipation = 1f;

	protected virtual void Initialisation() {
		col = GetComponent<SphereCollider>();
		rig = GetComponent<Rigidbody>();
		particleSys = GetComponent<ParticleSystem>();
	}

	private void FixedUpdate() {
		Dissipate();
	}

	protected void Dissipate() {
		col.radius = Mathf.Clamp(col.radius + dissipationRate * Time.fixedDeltaTime, 0, maxDissipation);
		if (col.radius >= maxDissipation)
			Despawn();
	}

	public void Spawn(Vector3 pos, Vector3 force) {
		Initialisation();
		transform.position = pos;
		rig.velocity = force;
		particleSys.Play();
	}

	protected void Despawn() {
		Destroy(gameObject);
	}

}
