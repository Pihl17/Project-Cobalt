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

	protected override void Initialisation() {
		base.Initialisation();
		weapons[0] = new RapidFireShot();
		weapons[1] = new MortarStrike();
	}

	void Update() {
		UpdateWeaponCooldowns();
	}

	protected void FindLockOnTarget() {
		throw new System.NotImplementedException();
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

}
