using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{

	float explodeRadius = 1;

	public void Fire(Vector3 velocity, float _damage, float _explodeRadius) {
		base.Fire(velocity, _damage);
		explodeRadius = _explodeRadius;
	}

	protected override void Initialization() {
		base.Initialization();
		OnImpact -= DamageCollision;
		OnImpact += Explode;
	}

	void Update() {
		CheckForSelfDestruct();
	}

	void Explode(Collision collision) {
		/*Debug.DrawRay(transform.position, Vector3.right * explodeRadius, Color.red, 2.0f);
		Debug.DrawRay(transform.position, -Vector3.right * explodeRadius, Color.red, 2.0f);
		Debug.DrawRay(transform.position, Vector3.forward * explodeRadius, Color.red, 2.0f);
		Debug.DrawRay(transform.position, -Vector3.forward * explodeRadius, Color.red, 2.0f);
		Debug.DrawRay(transform.position, Vector3.up * explodeRadius, Color.red, 2.0f);
		Debug.DrawRay(transform.position, -Vector3.up * explodeRadius, Color.red, 2.0f);*/

		Collider[] colInRange = Physics.OverlapSphere(transform.position, explodeRadius);
		for (int i = 0; i < colInRange.Length; i++) {
			DamageCollision(colInRange[i]);
		}
		GameObject.Destroy(gameObject);
	}

}
