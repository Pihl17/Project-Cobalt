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

		protected void ResetCooldown() {
			cooldownTimer = 0;
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
		public Transform target;
		public Vector3 targetVector;

		public Vector3 gunPosition;
		public Vector3 heavyPosition;
		public Vector3 artilleryPosition;

		public WeaponFireContext(InputActionPhase _triggerPhase, Transform _userTrans, Transform _target, MechConfig _mechConfig) {
			triggerPhase = _triggerPhase;
			userTrans = _userTrans;
			target = _target;
			targetVector = _target != null ? _target.position - _userTrans.position : _userTrans.forward * _mechConfig.LockOnDistrance;
			gunPosition = _userTrans.TransformDirection(_mechConfig.GunLocation);
			heavyPosition = _userTrans.TransformDirection(_mechConfig.HeavyLocation);
			artilleryPosition = _userTrans.TransformDirection(_mechConfig.ArtilleryLocation);
		}

	}
}