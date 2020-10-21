using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Abilities {

	public class RapidFireShot : Ability
	{
    
		//float fireRange = 10;
		//int damage = 1;

		public RapidFireShot() {
            configFile = Resources.Load<AbilityConfig>("AbilityConfigs/RapidFireShotConfig");
        }
	


		public override void Use(AbilityContext context) {
			if (ReadyToUse()) {
				if (context.triggerPhase == InputActionPhase.Started) {
					//Debug.Log("Started!");
					// Started can be used for auto-fire abilities

					Debug.DrawLine(context.userTrans.position, context.userTrans.position + context.targetVector.normalized*configFile.Range, Color.red, 1.0f);

					RaycastHit hit;
					if (Physics.Raycast(context.userTrans.position, context.targetVector.normalized, out hit, configFile.Range)) {
						if (hit.collider.GetComponent<EnemyScript>()) {
							hit.collider.GetComponent<EnemyScript>().Damage(configFile.Damage);
						}
					}
					cooldownTimer = 0;
				}
	
			}
		}
			
	}
}