using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {

	public class MissleLauncher : Weapon {

		protected GameObject newMissle;

		protected override void Firing(WeaponFireContext context) {
			newMissle = GameObject.Instantiate(configFile.InstantiatableObjects[0], context.userTrans.position + context.firePos, context.userTrans.rotation);
			newMissle.GetComponent<ExplosiveProjectile>().Fire(context.targetVector.normalized * configFile.Velocity, configFile.Damage, configFile.FloatValue[ValueName.ExplosionRadius]);

		}

	}
}