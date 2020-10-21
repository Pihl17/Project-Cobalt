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

		// TODO: Perhaps an interrupt function for when the player uses another ability in the middle of this one's duration, if for when an enemy interrupts them with an attack or the like?


	}


	public struct AbilityContext {
		public InputActionPhase triggerPhase;
		public Transform userTrans;
		public Vector3 targetVector;
		//public Vector3 targetPos;
		//public Vector3 faceDirection;
		//public Vector3 userPos;
		public RobotAbilityUseScript userScript;

		/*public AbilityContext(InputActionPhase _triggerPhase, Vector3 _faceDirection, Vector3 _userPos, RobotAbilityUseScript _userScript) {
			triggerPhase = _triggerPhase;
			faceDirection = _faceDirection;
			userPos = _userPos;
			userScript = _userScript;
		}*/

		public AbilityContext(InputActionPhase _triggerPhase, Transform _userTrans, Vector3 _targetVector, RobotAbilityUseScript _userScript) {
			triggerPhase = _triggerPhase;
			userTrans = _userTrans;
			targetVector = _targetVector;
			//targetPos = _targetPos;
			userScript = _userScript;
		}

	}
}