using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class CombatMech : Mech
{
	const int numberOfWeapons = 3;
	[SerializeReference] protected Weapon[] weapons = new Weapon[numberOfWeapons];
	public Weapon[] Weapons { get { return weapons; } }
	bool[] triggersHeldDown = new bool[numberOfWeapons];

	public RectTransform lockOnCrosshair;
	Vector2 crosshairOffCamPos = new Vector2(-200, -200);
	Transform lockOnTarget;
	const float minTargetSwitchAngle = 10;
	Collider[] possibleTargets;
	float lockOnMinAngle;
	Vector3 targetDir;
	float targetAngle;

	private void Start() {
		Initialisation();
	}

	protected override void Initialisation() {
		base.Initialisation();
	}

	void Update() {
		UpdateWeaponCooldowns();
		FindLockOnTarget();
	}

	void LateUpdate() {
		MoveLockOnCrosshair();
	}

	void CheckCurrentLockOnTarget() {
		lockOnMinAngle = mechConfig.LockOnAngle;
		if (lockOnTarget) {
			targetDir = lockOnTarget.position - transform.position;
			targetAngle = Vector3.Angle(transform.forward, targetDir);
			if (targetDir.magnitude <= mechConfig.LockOnDistrance && targetAngle <= mechConfig.LockOnAngle)
				lockOnMinAngle = targetAngle - minTargetSwitchAngle;
			else
				lockOnTarget = null;
		}
	}

	protected void FindLockOnTarget() {
		CheckCurrentLockOnTarget();

		possibleTargets = Physics.OverlapBox(transform.position + transform.forward * (mechConfig.LockOnDistrance / 2 + 1f), Vector3.one * mechConfig.LockOnDistrance / 2, transform.rotation);
		for (int i = 0; i < possibleTargets.Length; i++) {
			if (possibleTargets[i].GetComponent<IDestructible>() != null) {
				targetDir = possibleTargets[i].transform.position - transform.position;
				targetAngle = Vector3.Angle(transform.forward, targetDir);
				if (targetDir.magnitude <= mechConfig.LockOnDistrance && targetAngle < lockOnMinAngle) {
					lockOnTarget = possibleTargets[i].transform;
					lockOnMinAngle = targetAngle;
				}
			}
		}

	}

	protected void MoveLockOnCrosshair() {
		if (lockOnCrosshair) {
			if (lockOnTarget)
				lockOnCrosshair.anchoredPosition = Camera.main.WorldToScreenPoint(lockOnTarget.position);
			else
				lockOnCrosshair.anchoredPosition = crosshairOffCamPos;

		}
	}

	public void FireWeapon(int index) {
		weapons[index].Fire(DefineWeaponFireContext(index));
	}

	WeaponFireContext DefineWeaponFireContext(int index) {
		return new WeaponFireContext(transform, lockOnTarget, GetToTargetVector());
	}

	Vector3 GetToTargetVector() {
		return lockOnTarget != null ? lockOnTarget.position - transform.position : transform.forward * mechConfig.LockOnDistrance;
	}

	protected void UpdateWeaponCooldowns() {
		for (int i = 0; i < weapons.Length; i++)
			weapons[i].UpdateCooldown();
	}


	public void SetAutomaticFire(int index, bool startFire) {
		if (startFire)
			StartCoroutine(AutomaticFire(index, weapons[index].Cooldown));
		else
			triggersHeldDown[index] = false;
	}

	protected IEnumerator AutomaticFire(int index, float waitTime) {
		if (weapons[index] != null) {
			triggersHeldDown[index] = true;
			while (triggersHeldDown[index]) {
				FireWeapon(index);
				yield return new WaitForSeconds(waitTime);
			}
		}
	}

	void OnDrawGizmos() {
		if (lockOnTarget) {
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, lockOnTarget.position);
		}
	}


}
