using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {

	public class Flamethrower : Weapon {

		public override void Fire(WeaponFireContext context) {
			throw new System.NotImplementedException();
		}

		FlameParticle projectile;

		public void Fire(Vector3 firePoint, Vector3 direction) {
			if (ReadyToUse()) {
				projectile = GameObject.Instantiate(configFile.InstantiatableObjects[0]).GetComponent<FlameParticle>();
				projectile.Spawn(firePoint, direction.normalized * configFile.Velocity, configFile.Damage);

				PostFireUpdates();
			}
		}

	}
}