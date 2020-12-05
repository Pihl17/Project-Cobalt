using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

public class CombatMech : Mech
{
	protected Weapon[] weapons = new Weapon[2];
	public Weapon[] Weapons { get { return weapons; } }
	Transform lockOnTarget;
	Transform oldLockOnTarget;
	const float minTargetSwitchAngle = 5;
	Collider[] possibleTargets;
	float lockOnMinAngle;
	float lockOnAngle;
	Vector3 lockOnDir;
	

	protected override void Initialisation() {
		base.Initialisation();
		weapons[0] = new RapidFireShot();
		weapons[1] = new MortarStrike();
	}

	void Update() {
		UpdateWeaponCooldowns();
		FindLockOnTarget();
	}

	protected void FindLockOnTarget() {
		lockOnTarget = null;
		lockOnMinAngle = oldLockOnTarget && (oldLockOnTarget.position - transform.position).magnitude <= mechConfig.LockOnDistrance 
			? Vector3.Angle(transform.forward, (oldLockOnTarget.position - transform.position)) - minTargetSwitchAngle 
			: 180f;
		possibleTargets = Physics.OverlapBox(transform.position + transform.forward * (mechConfig.LockOnDistrance / 2 + 1f), Vector3.one * mechConfig.LockOnDistrance / 2, transform.rotation);
		for (int i = 0; i < possibleTargets.Length; i++) {
			if (possibleTargets[i].GetComponent<IDestructible>() != null && possibleTargets[i].GetComponent<IDestructible>().Targetable(mechConfig.Team)) {
				lockOnDir = possibleTargets[i].transform.position - transform.position;
				if (lockOnDir.magnitude <= mechConfig.LockOnDistrance)
				lockOnAngle = Vector3.Angle(transform.forward, lockOnDir);
				if (lockOnAngle < lockOnMinAngle) {
					lockOnTarget = possibleTargets[i].transform;
					lockOnMinAngle = lockOnAngle;
				}
			}
		}
		if (lockOnTarget == null)
			lockOnTarget = oldLockOnTarget;
		oldLockOnTarget = lockOnTarget;
	}

	protected void FireWeapon(int index, InputActionPhase phase) {
		weapons[index].Fire(DefineWeaponFireContext(phase));
	}

	WeaponFireContext DefineWeaponFireContext(InputActionPhase phase) {
		return new WeaponFireContext(phase, transform, lockOnTarget != null ? lockOnTarget.position - transform.position : transform.forward * mechConfig.LockOnDistrance, mechConfig);
	}

	public void ReplaceWeapon(int index, Weapon replacement) {
		if (index < weapons.Length)
			weapons[index] = replacement;
	}

	protected void UpdateWeaponCooldowns() {
		for (int i = 0; i < weapons.Length; i++)
			weapons[i].UpdateCooldown();
	}


	void OnDrawGizmos() {
		if (lockOnTarget) {
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, lockOnTarget.position);
		}
	}


}
