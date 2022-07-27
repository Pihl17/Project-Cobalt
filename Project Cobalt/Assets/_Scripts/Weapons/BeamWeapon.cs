using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Weapons
{

	[RequireComponent(typeof(LineRenderer))]
	public class BeamWeapon : Weapon
	{

		LineRenderer beamRender;
		Coroutine beamRemovalCoroutine;
		const float minBeamWidth = 0.05f;

		Vector3 firePos;
		Vector3 fireDir;
		Vector3 beamEndPoint;
		const int nonDestructibleLayers = ~(1 << 8); // Every layer except the "Destructible" layer

		protected override void Firing(WeaponFireContext context) {
			firePos = transform.position + localFirePoint;
			fireDir = (context.targetVector - localFirePoint).normalized;
			beamEndPoint = firePos + fireDir * configFile.Range;

			RaycastHit hit;
			if (Physics.Raycast(firePos, fireDir, out hit, configFile.Range, nonDestructibleLayers)) {
				beamEndPoint = hit.point;
			}

			Collider[] colliders = Physics.OverlapCapsule(firePos, beamEndPoint, configFile.FlatValue(ValueName.AOERadius));
			for (int i = 0; i < colliders.Length; i++) {
				if (colliders[i].transform != context.userTrans)
					ApplyDamageToEnemy(colliders[i], configFile.Damage);
			}

			ShowBeam(firePos, beamEndPoint);
		}


		protected override void Initialisation() {
			base.Initialisation();
				beamRender = GetComponent<LineRenderer>();
			if (beamRender) {
				beamRender.positionCount = 0;
				beamRender.startWidth = Mathf.Max(minBeamWidth, configFile.FlatValue(ValueName.AOERadius));
				beamRender.endWidth = Mathf.Max(minBeamWidth, configFile.FlatValue(ValueName.AOERadius));
			}
		}


		void ShowBeam(Vector3 startPos, Vector3 endPos) {
			if (!beamRender)
				return;
			beamRender.positionCount = 2;
			beamRender.SetPositions(new Vector3[] {startPos, endPos});
			if (beamRemovalCoroutine != null)
				StopCoroutine(beamRemovalCoroutine);
			beamRemovalCoroutine = StartCoroutine(RemoveBeam(configFile.Cooldown * 0.8f));
		}


		IEnumerator RemoveBeam(float beamDuration) {
			yield return new WaitForSeconds(beamDuration);
			beamRender.positionCount = 0;
		}


	}
}