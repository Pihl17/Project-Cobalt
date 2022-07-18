using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {

	public enum WeaponType { Light, Heavy, Artillery }

	[RequireComponent(typeof(AudioSource))]
	public abstract class Weapon : MonoBehaviour
	{

        [SerializeReference] protected WeaponConfig configFile;
		public WeaponConfig ConfigFile { get { return configFile; } }
		public float Cooldown { get { return configFile.Cooldown; } }

		protected float cooldownTimer;
		protected int ammo;
		public int Ammo { get { return ammo; } }
		protected int upgradeAmmo = 0;
		public int UpgradeAmmo { get { return upgradeAmmo; } }

		protected AudioSource effectSource;
		
		protected Vector3 localFirePoint;

		void Start() {
			Initialisation();
		}

		void Update() {
			UpdateCooldown();
		}

		protected void Initialisation() {
			effectSource = GetComponent<AudioSource>();
			ammo = configFile.MaxAmmo;
		}

		public void Fire(WeaponFireContext context) {
			if (ReadyToUse()) {
				localFirePoint = transform.TransformDirection(ConfigFile.LocalFirePoint);
				Firing(context);
				PostFireUpdates();
			}
		}

		protected abstract void Firing(WeaponFireContext context);

		public void SetConfigFile(WeaponConfig weaponConfig) {
			configFile = weaponConfig;
		}

        void UpdateCooldown() {
			cooldownTimer += Time.deltaTime;
		}

		protected bool ReadyToUse() {
			return cooldownTimer >= configFile.Cooldown && (configFile.UnlimitedAmmo || ammo > 0);
		}

		protected void PostFireUpdates() {
			cooldownTimer = 0;
			if (upgradeAmmo > 0) {
				upgradeAmmo--;
			} else if (!configFile.UnlimitedAmmo) {
				ammo--;
			}
		}

		public float GetCooldownLeftRatio() {
			return Mathf.Clamp(cooldownTimer/ configFile.Cooldown, 0, 1);
		}

		public string GetName() {
			return configFile.Name;
		}

		public void Upgrade() {
			upgradeAmmo = configFile.UpgradeMaxAmmo;
		}

		public static void ApplyDamageToEnemy(Collider collider, float amount) {
			if (collider.GetComponent<IDestructible>() != null) {
				collider.GetComponent<IDestructible>().Damage(amount);
			}
		}

		protected void PlaySoundEffect(WeaponConfig.SoundEffect effect) {
			if (effectSource) {
				effectSource.PlayOneShot(effect.clip, effect.volume);
			}
		}

	}

	


	public struct WeaponFireContext {
		public Transform userTrans;
		public Transform target;
		public Vector3 targetVector;

		public WeaponFireContext(Transform _userTrans, Transform _target, Vector3 _targetVector) {
			userTrans = _userTrans;
			target = _target;
			targetVector = _targetVector;
		}

	}
}
 