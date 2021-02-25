using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons {

	public class MortarLauncher : Weapon
	{

		public MortarLauncher() {
            configFile = Resources.Load<WeaponConfig>("WeaponConfigs/MortarStrikeConfig");
        }

		public override void Fire(WeaponFireContext context) {
			if (ReadyToUse()) {
				if (context.triggerPhase == InputActionPhase.Performed) {
					GameObject newBullet = GameObject.Instantiate(configFile.InstantiatableObjects[0], context.userTrans.position + context.artilleryPosition, Quaternion.identity);
					newBullet.GetComponent<ExplosiveProjectile>().Fire(CalculateMortarVelocity(context.targetVector - context.artilleryPosition, configFile.FloatValue[ValueName.MinCurvatureHeight]), configFile.Damage, configFile.FloatValue[ValueName.ExplosionRadius]);

					Debug.DrawLine(context.userTrans.position + context.artilleryPosition, context.userTrans.position + context.artilleryPosition + (context.targetVector - context.artilleryPosition), Color.red, 1.0f);

					cooldownTimer = 0;
				}
			}
		}


		public static Vector3 CalculateMortarVelocity(Vector3 dir, float minCurveHeight = 5) {
			float dist = dir.magnitude;

			float maxHeight = (dir.y + 1) * 2f;
			maxHeight = Mathf.Clamp(maxHeight, minCurveHeight, Mathf.Infinity);

			float verticalSpeed = Mathf.Sqrt(2 * -Physics.gravity.y * maxHeight);
			float travelTime = Mathf.Sqrt(2*(Mathf.Abs(maxHeight - dir.y)) / -Physics.gravity.y) + Mathf.Sqrt(2*maxHeight / -Physics.gravity.y);
			float horizontalSpeed = dist / travelTime;

			return new Vector3(dir.normalized.x * horizontalSpeed, verticalSpeed, dir.normalized.z * horizontalSpeed);
		}


	}
}