using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HomingGuidance : MonoBehaviour
{

	Rigidbody rig;

	Transform target;
	float seekForce = 4f;
	float maxVel = 2.5f;

	Vector3 steerDir;

	public void GiveTarget(Transform _target) {
		target = _target;
		if (!rig)
			rig = GetComponent<Rigidbody>();
	}

	public void GiveTarget(Transform _target, float _seekForce, float _maxVel) {
		GiveTarget(_target);
		seekForce = _seekForce;
		maxVel = _maxVel;
	}

	void Update() {
		SeekTarget();
	}

	void SeekTarget() {
		if (target) {
			steerDir = ((target.position - transform.position).normalized * maxVel - rig.velocity).normalized;
			rig.AddForce(steerDir * seekForce * Time.deltaTime, ForceMode.VelocityChange);
		}
		transform.LookAt(transform.position + rig.velocity);
	}

	void FixedUpdate() {
		LimitVelocity();
	}

	void LimitVelocity() {
		if (rig.velocity.magnitude > maxVel)
			rig.velocity = rig.velocity.normalized * maxVel;
	}
	

}
