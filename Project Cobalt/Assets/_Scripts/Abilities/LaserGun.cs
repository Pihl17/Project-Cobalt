using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Abilities {

	public class LaserGun : Ability
	{

		//float damagePerSecond = 3;
		//float fireRange = 10;
		RaycastHit hit;
        LineRenderer laserBeam;

		public LaserGun() {
            configFile = Resources.Load<AbilityConfig>("AbilityConfigs/LaserGunConfig");
        }

        public override void Use(AbilityContext context) {
            if (!laserBeam)
                laserBeam = GameObject.Instantiate(configFile.InstantiatableObjects[0]).GetComponent<LineRenderer>();
            if (ReadyToUse()) {
                laserBeam.SetPosition(0, context.userTrans.position);
                if (context.triggerPhase == InputActionPhase.Started)
                {
                    if (Physics.Raycast(context.userTrans.position, context.targetVector.normalized, out hit, configFile.Range))
                    {
                        laserBeam.SetPosition(1, hit.point);
						ApplyDamageToEnemy(hit.collider, Time.deltaTime * configFile.Damage);
                        /*if (hit.collider.GetComponent<EnemyScript>())
                        {
                            hit.collider.GetComponent<EnemyScript>().Damage(Time.deltaTime * configFile.Damage);
                        }*/
                    }
                    else {
                        laserBeam.SetPosition(1, context.userTrans.position + context.targetVector.normalized * configFile.Range);
                    }

                    //cooldownTimer = 0;
                }
                else if (context.triggerPhase == InputActionPhase.Canceled) {
                    laserBeam.SetPosition(1, context.userTrans.position);
                }
			}

		}

	}
}