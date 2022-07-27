using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {

	public class MortarLauncher : Weapon
	{

		/*public MortarLauncher() {
            configFile = Resources.Load<WeaponConfig>("WeaponConfigs/MortarStrikeConfig");
        }*/

		protected override void Firing(WeaponFireContext context) {
			GameObject newBullet = GameObject.Instantiate(configFile.InstantiatableObjects[0], transform.position + localFirePoint, Quaternion.identity);
			newBullet.GetComponent<ExplosiveProjectile>().Fire(CalculateMortarVelocity(context.targetVector - localFirePoint, configFile.FloatValue[ValueName.MinCurvatureHeight]), configFile.Damage, configFile.FloatValue[ValueName.AOERadius]);

			Debug.DrawLine(transform.position + localFirePoint, transform.position + localFirePoint + (context.targetVector - localFirePoint), Color.red, 1.0f);
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