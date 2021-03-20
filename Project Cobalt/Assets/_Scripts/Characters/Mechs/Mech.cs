using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : Vehicle
{

	public MechConfig mechConfig;

	public override float GetMaxHealth() {
		return mechConfig.MaxHealth;
	}

	protected override float GetMaxVel() {
		return mechConfig.MaxMoveSpeed;
	}

	public void Walk(Vector2 direction) {
		if (direction == Vector2.zero) {
			StopMovement();
			return;
		}
		direction *= mechConfig.MoveSpeed;
		rig.AddForce(transform.right * direction.x + transform.forward * direction.y, ForceMode.VelocityChange);
		LimitMoveSpeed();
	}

	protected void StopMovement() {
		rig.velocity = Vector3.up * rig.velocity.y;
	}

	public override void Turn(float rightAngle) {
		base.Turn(rightAngle * mechConfig.TurnSpeed);
	}



}
