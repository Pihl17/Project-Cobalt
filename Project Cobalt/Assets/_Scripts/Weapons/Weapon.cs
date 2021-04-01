using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {

	public abstract class Weapon
	{

        protected WeaponConfig configFile;
		public float Cooldown { get { return configFile.Cooldown; } }

		protected float cooldownTimer;
		protected int upgradeAmmo = 0;

		protected AudioSource effectSource;

		public void Fire(WeaponFireContext context) {
			if (ReadyToUse()) {
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

		public void SetEffectSource(AudioSource audioSource) {
			effectSource = audioSource;
		}

		protected void PlaySoundEffect(WeaponConfig.SoundEffect effect) {
			if (effectSource) {
				effectSource.PlayOneShot(effect.clip, effect.volume);
			}
		}

		public enum WeaponType { MachineGun, MissleLauncher }
		public static Weapon DefineType(WeaponType weaponType) {
			switch (weaponType) {
				case WeaponType.MachineGun:
					return new MachineGun();
				case WeaponType.MissleLauncher:
					return new MissleLauncher();
			}
			Debug.LogError("WeaponAdaptor.DefineType does not return any weapons of type " + weaponType.ToString() + " - ADD IT TO THE FUNCTION!");
			throw new System.Exception();
		}

		public static Weapon DefineType(WeaponType weaponType, WeaponConfig config) {
			Weapon weapon = DefineType(weaponType);
			weapon.SetConfigFile(config);
			return weapon;
		}
		


	}

	


	public struct WeaponFireContext {
		public Transform userTrans;
		public Transform target;
		public Vector3 targetVector;
		public Vector3 firePos;

		public WeaponFireContext(Transform _userTrans, Transform _target, Vector3 _targetVector, Vector3 _localFirePos) {
			userTrans = _userTrans;
			target = _target;
			targetVector = _targetVector;
			firePos = _userTrans.TransformDirection(_localFirePos);
		}

	}
}
 