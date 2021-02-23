using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameParticle : DissipatingParticle 
{

	float damagePerSecondPerParticle = 1f;

	private void FixedUpdate() {
		Dissipate();
	}

	public void Spawn(Vector3 pos, Vector3 force, float _damagePerSecond) {
		Spawn(pos, force);
		damagePerSecondPerParticle = _damagePerSecond;
	}

	private void OnTriggerStay(Collider other) {
		Projectile.DamageCollision(other, damagePerSecondPerParticle * Time.fixedDeltaTime);
	}

}
