using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {

	public class HomingMissleLauncher : MissleLauncher {

		public HomingMissleLauncher() {
			configFile = Resources.Load<WeaponConfig>("WeaponConfigs/HomingMisslesConfig");
		}

		protected override void Firing(WeaponFireContext context) {
			base.Firing(context);
			newMissle.GetComponent<HomingGuidance>().GiveTarget(context.target, configFile.FloatValue[ValueName.SeekForce], configFile.FloatValue[ValueName.MaxVelocity]);
		}

	}
}