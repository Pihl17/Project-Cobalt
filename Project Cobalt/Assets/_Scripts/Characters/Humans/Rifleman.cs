using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class Rifleman : Human, IDetectingUnit
{

	Weapon gun;

	Transform target;
	Vector3 toTarget;

	protected override void Initialise() {
		base.Initialise();
		gun = Weapon.DefineType(Weapon.WeaponType.MachineGun, config.WeaponConfig);
	}

	// Update is called once per frame
	void Update()
    {
		gun.UpdateCooldown();
		if (target) {
			toTarget = target.position - transform.position;
			if (toTarget.magnitude <= config.WeaponConfig.Range) {
				FireAtTarget();
			}
		}
    }

	void FixedUpdate() {
		if (target) {
			toTarget = target.position - transform.position;
			TurnTowardsTarget();
			if (toTarget.magnitude > config.WeaponConfig.Range) {
				MoveTowardsTarget();
			}
		}
	}

	public float GetDetectionRange() {
		return config.DetectionRange;
	}

	public void AddTarget(Transform _target) {
		target = _target;
	}

	public void RemoveTarget(Transform _target) {
		if (target == _target) target = null;
	}

	void MoveTowardsTarget() {
		Move(toTarget);
	}

	void TurnTowardsTarget() {
		transform.LookAt(target.position, Vector3.up);
	}

	void FireAtTarget() {
		gun.Fire(new WeaponFireContext(transform, target, toTarget, Vector3.forward * 0.25f));
	}

}
