using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {

	public class MachineGun : Weapon
	{

		ParticleSystem muzzleFlashEffect;
		ParticleSystem impactEffect;
		ParticleSystem upgradeImpactEffect;
		Vector3 firePos;
		Vector3 fireDir;
	


		protected override void Firing(WeaponFireContext context) {
			InitialisateEffects(context.userTrans);
			firePos = transform.position + localFirePoint;
			fireDir = (context.targetVector - localFirePoint).normalized;


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

			if (configFile.AudioEffects.Length > 0)
				PlaySoundEffect(configFile.AudioEffects[Random.Range(0, configFile.AudioEffects.Length)]);

		}

		public void InitialisateEffects(Transform muzzleTrans) {
			if (!muzzleFlashEffect)
				muzzleFlashEffect = GameObject.Instantiate(configFile.InstantiatableObjects[0], muzzleTrans).GetComponent<ParticleSystem>();
			if (!impactEffect)
				impactEffect = GameObject.Instantiate(configFile.InstantiatableObjects[1]).GetComponent<ParticleSystem>();
			if (!upgradeImpactEffect)
				upgradeImpactEffect = GameObject.Instantiate(configFile.InstantiatableObjects[2]).GetComponent<ParticleSystem>();
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