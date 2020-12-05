using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons {

	public abstract class Weapon
	{

        protected WeaponConfig configFile;
		public float Cooldown { get { return configFile.Cooldown; } }

		protected float cooldownTimer;

		//public Coroutine coroutine;
		[HideInInspector] public bool triggerHeldDown;

		public abstract void Fire(WeaponFireContext context);


        public void UpdateCooldown() {
			cooldownTimer += Time.deltaTime;
		}

		protected bool ReadyToUse() {
			return cooldownTimer >= configFile.Cooldown;
		}

		public float GetCooldownLeftRatio() {
			return Mathf.Clamp(cooldownTimer/ configFile.Cooldown, 0, 1);
		}

		public string GetName() {
			return configFile.Name;
		}

		public static void ApplyDamageToEnemy(Collider collider, float amount) {
			if (collider.GetComponent<IDestructible>() != null) {
				collider.GetComponent<IDestructible>().Damage(amount);
			}
		}

	}


	public struct WeaponFireContext {
		public InputActionPhase triggerPhase;
		public Transform userTrans;
		public Vector3 targetVector;
		public MechConfig mechConfig;

		public Vector3 GunPosition { get { return userTrans.TransformDirection(mechConfig.GunLocation); } }

		public WeaponFireContext(InputActionPhase _triggerPhase, Transform _userTrans, Vector3 _targetVector, MechConfig _mechConfig) {
			triggerPhase = _triggerPhase;
			userTrans = _userTrans;
			targetVector = _targetVector;
			mechConfig = _mechConfig;
		}

	}
}