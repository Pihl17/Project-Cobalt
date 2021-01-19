using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;


public class HomingMissleLauncher : Weapon
{

	public HomingMissleLauncher() {
		configFile = Resources.Load<WeaponConfig>("WeaponConfigs/HomingMisslesConfig");
	}

	public override void Fire(WeaponFireContext context) {
		if (ReadyToUse()) {
			if (context.triggerPhase == UnityEngine.InputSystem.InputActionPhase.Started) {

				HomingMissile newMissle = GameObject.Instantiate(configFile.InstantiatableObjects[0], context.userTrans.position + context.heavyPosition, context.userTrans.rotation).GetComponent<HomingMissile>();
				newMissle.Fire(newMissle.transform.forward * configFile.Velocity, configFile.Damage, configFile.FloatValue[ValueName.ExplosionRadius], context.target, configFile.FloatValue[ValueName.SeekForce], configFile.FloatValue[ValueName.MaxVelocity]);

				ResetCooldown();
			}
		}
	}

}
