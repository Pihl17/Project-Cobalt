using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {

	[RequireComponent(typeof(AudioSource))]
	public abstract class Weapon : MonoBehaviour
	{

        [SerializeReference] protected WeaponConfig configFile;
		public WeaponConfig ConfigFile { get { return configFile; } }
		public float Cooldown { get { return configFile.Cooldown; } }

		protected float cooldownTimer;
		protected int upgradeAmmo = 0;

		protected AudioSource effectSource;

		protected Vector3 localFirePoint;

		void Start() {
			Initialisation();
		}

		protected void Initialisation() {
			effectSource = GetComponent<AudioSource>();
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

        public void UpdateCooldown() {
			cooldownTimer += Time.deltaTime;
		}

		protected bool ReadyToUse() {
			return cooldownTimer >= configFile.Cooldown;
		}

		protected void PostFireUpdates() {
			cooldownTimer = 0;
			if (upgradeAmmo > 0)
				upgradeAmmo--;
		}

		public float GetCooldownLeftRatio() {
			return Mathf.Clamp(cooldownTimer/ configFile.Cooldown, 0, 1);
		}

		public string GetName() {
			return configFile.Name;
		}

		public int GetUpgradeMaxAmmo() {
			return configFile.UpgradeMaxAmmo;
		}

		public int GetUpgradeAmmo() {
			return upgradeAmmo;
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
 