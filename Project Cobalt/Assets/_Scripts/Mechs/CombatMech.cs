using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

public class CombatMech : Mech
{
	const int numberOfWeapons = 3;
	protected Weapon[] weapons = new Weapon[numberOfWeapons];
	public Weapon[] Weapons { get { return weapons; } }
	public RectTransform lockOnCrosshair;
	Vector2 crosshairOffCamPos = new Vector2(-200, -200);
	Transform lockOnTarget;
	Transform oldLockOnTarget;
	const float minTargetSwitchAngle = 10;
	Collider[] possibleTargets;
	float lockOnMinAngle;
	float lockOnAngle;
	Vector3 lockOnDir;
	bool lastTargetIsInRange;


	protected override void Initialisation() {
		base.Initialisation();
		weapons[0] = new RapidFireShot();
		weapons[1] = new HomingMissleLauncher();
		weapons[2] = new MortarStrike();
	}

	void Update() {
		UpdateWeaponCooldowns();
		FindLockOnTarget();
	}

	void LateUpdate() {
		MoveLockOnCrosshair();
	}

	protected void FindLockOnTarget() {
		lockOnTarget = null;
		lastTargetIsInRange = false;
		lockOnMinAngle = oldLockOnTarget && (oldLockOnTarget.position - transform.position).magnitude <= mechConfig.LockOnDistrance 
			? Vector3.Angle(transform.forward, (oldLockOnTarget.position - transform.position)) - minTargetSwitchAngle 
			: 180f;
		possibleTargets = Physics.OverlapBox(transform.position + transform.forward * (mechConfig.LockOnDistrance / 2 + 1f), Vector3.one * mechConfig.LockOnDistrance / 2, transform.rotation);
		for (int i = 0; i < possibleTargets.Length; i++) {
			if (possibleTargets[i].transform.Equals(oldLockOnTarget)) {
				lastTargetIsInRange = true;
				continue;
			}
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
		if (lastTargetIsInRange && lockOnTarget == null)
			lockOnTarget = oldLockOnTarget;
		oldLockOnTarget = lockOnTarget;
	}

	protected void MoveLockOnCrosshair() {
		if (lockOnCrosshair) {
			if (lockOnTarget)
				lockOnCrosshair.anchoredPosition = Camera.main.WorldToScreenPoint(lockOnTarget.position);
			else
				lockOnCrosshair.anchoredPosition = crosshairOffCamPos;

		}
	}

	protected void FireWeapon(int index, InputActionPhase phase) {
		weapons[index].Fire(DefineWeaponFireContext(index, phase));
	}

	WeaponFireContext DefineWeaponFireContext(int index, InputActionPhase phase) {
		return new WeaponFireContext(phase, transform, lockOnTarget, mechConfig, 0 /*upgradeAmmo[index]*/);
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
