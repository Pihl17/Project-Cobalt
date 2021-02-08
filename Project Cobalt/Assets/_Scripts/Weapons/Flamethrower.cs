using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {

	public class Flamethrower : Weapon {

		public override void Fire(WeaponFireContext context) {
			throw new System.NotImplementedException();
		}

		public void Fire(Vector3 direction) {
			if (ReadyToUse()) {
				throw new System.NotImplementedException();
				PostFireUpdates();
			}
		}

	}
}