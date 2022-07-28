using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons.Projectiles;

namespace Weapons {

	public class HomingMissleLauncher : MissleLauncher {

		protected override void Firing(WeaponFireContext context) {
			base.Firing(context);
			newMissle.GetComponent<HomingGuidance>().GiveTarget(context.target, configFile.FloatValue[ValueName.SeekForce], configFile.FloatValue[ValueName.MaxVelocity]);
		}

	}
}