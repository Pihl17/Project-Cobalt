using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Abilities {

	public abstract class Ability
	{

        protected AbilityConfig configFile;

		protected float cooldownTimer;


		public abstract void Use(AbilityContext context);


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
			if (collider.GetComponent<IDamageable>() != null) {
				collider.GetComponent<IDamageable>().Damage(amount);
			}
		}

	}


	public struct AbilityContext {
		public InputActionPhase triggerPhase;
		public Transform userTrans;
		public Vector3 targetVector;
		public MechWeaponSystemScript userScript;

		public AbilityContext(InputActionPhase _triggerPhase, Transform _userTrans, Vector3 _targetVector, MechWeaponSystemScript _userScript) {
			triggerPhase = _triggerPhase;
			userTrans = _userTrans;
			targetVector = _targetVector;
			//targetPos = _targetPos;
			userScript = _userScript;
		}

	}
}