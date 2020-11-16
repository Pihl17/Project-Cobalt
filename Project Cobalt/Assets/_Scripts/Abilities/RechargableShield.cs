using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Abilities {

	public class RechargableShield : Ability
	{

		//int chargeAmount = 4;

		public RechargableShield() {
            configFile = Resources.Load<AbilityConfig>("AbilityConfigs/RechargableShieldConfig");
        }

		public override void Use(AbilityContext context) {
			if (ReadyToUse()) {
				if (context.triggerPhase == InputActionPhase.Canceled) {

					//context.userScript.GainShield(chargeAmount);
					cooldownTimer = 0;
				}
			}
		}

	}
}
