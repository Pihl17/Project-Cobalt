using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons {

	public class MachineGun : Weapon
	{

		//float fireRange = 10;
		//int damage = 1;
		ParticleSystem muzzleFlashEffect;
		ParticleSystem impactEffect;
		ParticleSystem upgradeImpactEffect;
		Vector3 firePos;

		public MachineGun() {
            configFile = Resources.Load<WeaponConfig>("WeaponConfigs/RapidFireShotConfig");
        }
	


		public override void Fire(WeaponFireContext context) {
			if (context.triggerPhase == InputActionPhase.Started) {
				// Started can be used for auto-fire abilities
				InitialisateEffects(context.userTrans);
				Fire(context.userTrans.position + context.gunPosition, (context.targetVector - context.gunPosition).normalized);
			}

			/*if (!muzzleFlashEffect)
				muzzleFlashEffect = GameObject.Instantiate(configFile.InstantiatableObjects[0],context.userTrans).GetComponent<ParticleSystem>();
			if (!impactEffect)
				impactEffect = GameObject.Instantiate(configFile.InstantiatableObjects[1]).GetComponent<ParticleSystem>();
			if (!upgradeImpactEffect)
				upgradeImpactEffect = GameObject.Instantiate(configFile.InstantiatableObjects[2]).GetComponent<ParticleSystem>();
			if (ReadyToUse()) {
				if (context.triggerPhase == InputActionPhase.Started) {
					//Debug.Log("Started!");
					// Started can be used for auto-fire abilities

					firePos = context.userTrans.position + context.gunPosition;//context.userTrans.TransformDirection(context.mechConfig.GunLocation);
					
					//Debug.DrawLine(firePos, firePos + context.targetVector.normalized*configFile.Range, Color.red, 1.0f);
					//Debug.DrawRay(firePos, (context.targetVector - context.userScript.GunLocation).normalized * configFile.Range, Color.red, 1.0f);
					muzzleFlashEffect.transform.position = firePos;
					muzzleFlashEffect.transform.LookAt(firePos + context.targetVector.normalized);
					muzzleFlashEffect.Play();

					RaycastHit hit;
					if (Physics.Raycast(firePos, (context.targetVector - context.gunPosition).normalized, out hit, configFile.Range)) {
						if (upgradeAmmo > 0) {
							ApplyDamageToEnemy(hit.collider, configFile.Damage * configFile.UpgradeDamageMultiplier);
							PlayImpactEffect(upgradeImpactEffect, hit);
						} else {
							ApplyDamageToEnemy(hit.collider, configFile.Damage);
							PlayImpactEffect(impactEffect, hit);
						}
					}
					PostFireUpdates();
				}
	
			}*/
		}

		public void InitialisateEffects(Transform muzzleTrans) {
			if (!muzzleFlashEffect)
				muzzleFlashEffect = GameObject.Instantiate(configFile.InstantiatableObjects[0], muzzleTrans).GetComponent<ParticleSystem>();
			if (!impactEffect)
				impactEffect = GameObject.Instantiate(configFile.InstantiatableObjects[1]).GetComponent<ParticleSystem>();
			if (!upgradeImpactEffect)
				upgradeImpactEffect = GameObject.Instantiate(configFile.InstantiatableObjects[2]).GetComponent<ParticleSystem>();
		}

		public void Fire(Vector3 firePos, Vector3 fireDir) {
			
			if (ReadyToUse()) {
				//Debug.DrawLine(firePos, firePos + context.targetVector.normalized*configFile.Range, Color.red, 1.0f);
				//Debug.DrawRay(firePos, (context.targetVector - context.userScript.GunLocation).normalized * configFile.Range, Color.red, 1.0f);
				if (muzzleFlashEffect) {
					muzzleFlashEffect.transform.position = firePos;
					muzzleFlashEffect.transform.LookAt(firePos + fireDir);
					muzzleFlashEffect.Play();
				}

				RaycastHit hit;
				if (Physics.Raycast(firePos, fireDir, out hit, configFile.Range)) {
					if (upgradeAmmo > 0) {
						ApplyDamageToEnemy(hit.collider, configFile.Damage * configFile.UpgradeDamageMultiplier);
						PlayImpactEffect(upgradeImpactEffect, hit);
					} else {
						ApplyDamageToEnemy(hit.collider, configFile.Damage);
						PlayImpactEffect(impactEffect, hit);
					}
				}
				PostFireUpdates();
			}
		}

		void PlayImpactEffect(ParticleSystem particle, RaycastHit hit) {
			if (particle) {
				particle.transform.position = hit.point;
				particle.transform.LookAt(hit.point + hit.normal);
				particle.Play();
			}
		}

			
	}
}