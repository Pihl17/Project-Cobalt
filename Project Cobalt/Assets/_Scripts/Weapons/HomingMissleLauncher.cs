using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;


public class HomingMissleLauncher : Weapon
{

	public HomingMissleLauncher() {
		configFile = Resources.Load<WeaponConfig>("WeaponConfigs/HomingMisslesConfig");
	}

	protected override void Firing(WeaponFireContext context) {

		GameObject newMissle = GameObject.Instantiate(configFile.InstantiatableObjects[0], context.userTrans.position + context.firePos, context.userTrans.rotation);
		newMissle.GetComponent<ExplosiveProjectile>().Fire(newMissle.transform.forward * configFile.Velocity, configFile.Damage, configFile.FloatValue[ValueName.ExplosionRadius]);
		newMissle.GetComponent<HomingGuidance>().GiveTarget(context.target, configFile.FloatValue[ValueName.SeekForce], configFile.FloatValue[ValueName.MaxVelocity]);
	}

}
