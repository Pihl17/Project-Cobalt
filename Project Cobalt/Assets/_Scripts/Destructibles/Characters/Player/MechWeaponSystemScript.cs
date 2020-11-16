using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Abilities;

public class MechWeaponSystemScript : MechSystemScript {

	protected Ability[] ability = new Ability[2];

	[SerializeField] Vector3 gunLocation = new Vector3(0, 1.21f, 1.16f);
	public Vector3 GunLocation { get { return transform.TransformDirection(gunLocation); } }
	[SerializeField] Vector3 launcherLocation = new Vector3(0, 2.06f, 0);
	public Vector3 LauncherLocation { get { return transform.TransformDirection(launcherLocation); } }

	protected override void Initialisation() {
		base.Initialisation();
		ability[0] = new RapidFireShot();
		ability[1] = new MortarStrike();
	}

	public void UseAbility(int n, InputActionPhase phase) {
		ability[n].Use(GetAbilityContext(phase));
	}

	protected AbilityContext GetAbilityContext(InputActionPhase phase) {
		//return new AbilityContext(phase, turningPart.forward, transform.position, this);
		return new AbilityContext(phase, transform, lockOnTarget != null ? lockOnTarget.position - transform.position : transform.forward * configFile.LockOnDistrance, this);
		/*if (lockOnTarget)
			return new AbilityContext(phase, transform, (lockOnTarget.position - transform.position).normalized, lockOnTarget.position, this);
		else
			return new AbilityContext(phase, transform, transform.forward, transform.position + transform.forward * lockOnDistance, this);*/
	}

	public void UpdateAbilityCooldowns() {
		for (int i = 0; i < ability.Length; i++) {
			ability[i].UpdateCooldown();
		}
	}

	public int GetAbilityCount() {
		return ability.Length;
	}

	public Ability[] GetAbilities() {
		return ability;
	}

	public Ability GetAbility(int index) {
		return ability[index];
	}

	public void SwitchAbility(int index, Ability newAbility) {
		if (index < ability.Length)
			ability[index] = newAbility;
	}



}
