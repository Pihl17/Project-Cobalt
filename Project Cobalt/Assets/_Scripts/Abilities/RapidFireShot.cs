using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Abilities {

	public class RapidFireShot : Ability
	{

		//float fireRange = 10;
		//int damage = 1;
		ParticleSystem muzzleFlashEffect;
		ParticleSystem impactEffect;
		Vector3 firePos;

		public RapidFireShot() {
            configFile = Resources.Load<AbilityConfig>("AbilityConfigs/RapidFireShotConfig");
        }
	


		public override void Use(AbilityContext context) {
			if (!muzzleFlashEffect)
				muzzleFlashEffect = GameObject.Instantiate(configFile.InstantiatableObjects[0],context.userTrans).GetComponent<ParticleSystem>();
			if (!impactEffect)
				impactEffect = GameObject.Instantiate(configFile.InstantiatableObjects[1]).GetComponent<ParticleSystem>();
			if (ReadyToUse()) {
				if (context.triggerPhase == InputActionPhase.Started) {
					//Debug.Log("Started!");
					// Started can be used for auto-fire abilities

					firePos = context.userTrans.position + context.userScript.GunLocation;
					
					//Debug.DrawLine(firePos, firePos + context.targetVector.normalized*configFile.Range, Color.red, 1.0f);
					//Debug.DrawRay(firePos, (context.targetVector - context.userScript.GunLocation).normalized * configFile.Range, Color.red, 1.0f);
					muzzleFlashEffect.transform.position = firePos;
					muzzleFlashEffect.transform.LookAt(firePos + context.targetVector.normalized);
					muzzleFlashEffect.Play();

					RaycastHit hit;
					if (Physics.Raycast(firePos, (context.targetVector - context.userScript.GunLocation).normalized, out hit, configFile.Range)) {
						ApplyDamageToEnemy(hit.collider, configFile.Damage);
						/*if (hit.collider.GetComponent<DestructableScript>()) {
							hit.collider.GetComponent<DestructableScript>().Damage(configFile.Damage);
						}*/
						impactEffect.transform.position = hit.point;
						impactEffect.transform.LookAt(hit.point + hit.normal);
						impactEffect.Play();
					}
					cooldownTimer = 0;
				}
	
			}
		}
			
	}
}