using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Abilities {

	public class MortarStrike : Ability
	{

		//int bulletDamage = 10;
		//float bulletStartHeight = 2;
		//float bulletExplodeRadius = 1.5f;

		public MortarStrike() {
            configFile = Resources.Load<AbilityConfig>("AbilityConfigs/MortarStrikeConfig");
        }

		public override void Use(AbilityContext context) {
			if (ReadyToUse()) {
				if (context.triggerPhase == InputActionPhase.Performed) {
					GameObject newBullet = GameObject.Instantiate(configFile.InstantiatableObjects[0], context.userTrans.position + context.userScript.LauncherLocation, Quaternion.identity);
					newBullet.GetComponent<MortarBulletScript>().Fire(CalculateMortarVelocity(context.targetVector - context.userScript.LauncherLocation, configFile.FloatValue[ValueName.MinCurvatureHeight]), configFile.Damage, configFile.FloatValue[ValueName.ExplosionRadius]);

					Debug.DrawLine(context.userTrans.position + context.userScript.LauncherLocation, context.userTrans.position + context.userScript.LauncherLocation + (context.targetVector - context.userScript.LauncherLocation), Color.red, 1.0f);

					cooldownTimer = 0;
				}
			}
		}


		public static Vector3 CalculateMortarVelocity(Vector3 dir, float minCurveHeight = 5) {
			//float dist = new Vector3(dir.x, 0, dir.z).magnitude;
			float dist = dir.magnitude;

			float maxHeight = (dir.y + 1) * 2f;
			maxHeight = Mathf.Clamp(maxHeight, minCurveHeight, Mathf.Infinity);

			float verticalSpeed = Mathf.Sqrt(2 * -Physics.gravity.y * maxHeight);
			float travelTime = Mathf.Sqrt(2*(Mathf.Abs(maxHeight - dir.y)) / -Physics.gravity.y) + Mathf.Sqrt(2*maxHeight / -Physics.gravity.y);
			float horizontalSpeed = dist / travelTime;

			//Debug.Log("Vertical speed: " + verticalSpeed);
			//Debug.Log("Travel time: " + travelTime);
			//Debug.Log("Horizontal speed: " + horizontalSpeed);

			return new Vector3(dir.normalized.x * horizontalSpeed, verticalSpeed, dir.normalized.z * horizontalSpeed);
		}


	}
}