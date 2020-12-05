using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons {

	public class RechargableShield : Weapon
	{

		//int chargeAmount = 4;

		public RechargableShield() {
            configFile = Resources.Load<WeaponConfig>("WeaponConfigs/RechargableShieldConfig");
        }

		public override void Fire(WeaponFireContext context) {
			if (ReadyToUse()) {
				if (context.triggerPhase == InputActionPhase.Canceled) {

					//context.userScript.GainShield(chargeAmount);
					cooldownTimer = 0;
				}
			}
		}

	}
}
