using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCharacterScript : CharacterScript
{
    
	public Transform turningPart;

	protected Transform lockOnTarget;
	protected float lockOnDistance = 15.0f;


	public void Turn(Vector3 faceDirection) {
		turningPart.eulerAngles = new Vector3(0, Vector3.SignedAngle(Vector3.forward, faceDirection, Vector3.up), 0);
	}


	protected void FindLockOnTarget() {
		lockOnTarget = null;
		Collider[] possibleTargets = Physics.OverlapBox(transform.position + transform.forward * (lockOnDistance/2 + 1f), Vector3.one * lockOnDistance/2, transform.rotation);
		float tempAngle;
		float tempMinAngle = 90;
		for (int i = 0; i < possibleTargets.Length; i++) {
			if (possibleTargets[i].GetComponent<DestructableScript>() && (possibleTargets[i].transform.position - transform.position).magnitude <= lockOnDistance) {
				tempAngle = Vector3.Angle(transform.forward, possibleTargets[i].transform.position - transform.position);
				if (tempAngle < tempMinAngle) {
					lockOnTarget = possibleTargets[i].transform;
					tempMinAngle = tempAngle;
				}
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		//Gizmos.DrawWireCube(transform.position + transform.forward * (lockOnDistance/2 + 1f), Vector3.one * lockOnDistance);
		if (lockOnTarget)
			Gizmos.DrawLine(transform.position, lockOnTarget.position);
	}


}
