using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{

	[RequireComponent(typeof(LineRenderer))]
	public class LightningWeapon : Weapon
	{

		LineRenderer LightningRender;
		Coroutine LightningRemovalCoroutine;

		Vector3 firePos;
		Vector3 fireDir;
		RaycastHit hit;

		protected override void Firing(WeaponFireContext context) {
			firePos = transform.position + localFirePoint;
			fireDir = (context.targetVector - localFirePoint).normalized;


			if (Physics.Raycast(firePos, fireDir, out hit, configFile.Range)) {
				ApplyDamageToEnemy(hit.collider, configFile.Damage);
				ShowLightning(firePos, hit.point);
			} else {
				ShowLightning(firePos, firePos + fireDir * configFile.Range);
			}


		}

		protected override void Initialisation() {
			base.Initialisation();
			LightningRender = GetComponent<LineRenderer>();
			if (LightningRender) {
				LightningRender.positionCount = 0;
			}
		}

		const float disPerLineSegment = 1.0f;
		const float lineDisplacementDis = 0.2f;
		void ShowLightning(Vector3 startPos, Vector3 endPos) {
			if (!LightningRender)
				return;

			Vector3 line = endPos - startPos;
			Vector3 rightLine = new Vector3(line.z, line.y, -line.x).normalized;

			Vector3[] lineVertices = new Vector3[Mathf.RoundToInt(line.magnitude / disPerLineSegment)];
			float segmentLength = line.magnitude / (lineVertices.Length - 1);

			for (int i = 0; i < lineVertices.Length; i++) {
				lineVertices[i] = startPos + line.normalized * i * segmentLength + rightLine * Random.Range(-lineDisplacementDis, lineDisplacementDis);
			}

			LightningRender.positionCount = lineVertices.Length;
			LightningRender.SetPositions(lineVertices);
			if (LightningRemovalCoroutine != null)
				StopCoroutine(LightningRemovalCoroutine);
			LightningRemovalCoroutine = StartCoroutine(RemoveLightning(configFile.Cooldown));
		}


		IEnumerator RemoveLightning(float beamDuration) {
			yield return new WaitForSeconds(beamDuration);
			LightningRender.positionCount = 0;
		}


	}
}