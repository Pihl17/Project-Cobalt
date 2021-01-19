using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : ExplosiveProjectile
{

	public Transform target;
	float seekForce = 4f;
	float maxVel = 2.5f;

	public void Fire(Vector3 startVelocity, float _damage, float _explodeRadius, Transform _target, float _seekForce, float _maxVel) {
		base.Fire(startVelocity, _damage, _explodeRadius);
		target = _target;
		seekForce = _seekForce;
		maxVel = _maxVel;
	}

	protected override void Initialization() {
		base.Initialization();
		timeAlive = 10;
	}

	void Update() {
		SeekTarget();
		CheckForSelfDestruct();
	}

	void FixedUpdate() {
		if (rig.velocity.magnitude > maxVel)
			rig.velocity = rig.velocity.normalized * maxVel;
	}

	void SeekTarget() {
		if (target) {
			/*Debug.DrawRay(transform.position, rig.velocity, Color.green);
			Debug.DrawRay(transform.position, (target.position - transform.position).normalized * maxVel, Color.blue);
			Debug.DrawRay(transform.position + rig.velocity, (target.position - transform.position).normalized * maxVel - rig.velocity, Color.red);*/
			rig.AddForce( ((target.position - transform.position).normalized * maxVel - rig.velocity).normalized * seekForce * Time.deltaTime, ForceMode.VelocityChange);
		}
		transform.LookAt(transform.position + rig.velocity);
	}

}
