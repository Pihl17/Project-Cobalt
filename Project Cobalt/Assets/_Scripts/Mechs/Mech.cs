using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : Vehicle
{

	public MechConfig mechConfig;

	protected override void Initialisation() {
		base.Initialisation();
	}

	public override float GetMaxHealth() {
		return mechConfig.MaxHealth;
	}

	protected override float GetMaxVel() {
		return mechConfig.MaxMoveSpeed;
	}

	protected void Move(Vector2 direction) {
		if (direction == Vector2.zero) {
			StopMovement();
			return;
		}
		Strafe(direction.x * mechConfig.MoveSpeed);
		Drive(direction.y * mechConfig.MoveSpeed);
	}

	void Strafe(float right) {
		rig.AddForce(transform.right * right, ForceMode.VelocityChange);
	}

	protected override void Drive(float forward) {
		rig.AddForce(transform.forward * forward, ForceMode.VelocityChange);
		LimitSpeed();
	}

	protected void StopMovement() {
		rig.velocity = Vector3.up * rig.velocity.y;
	}

	protected override void Turn(float rightAngle) {
		base.Turn(rightAngle * mechConfig.TurnSpeed);
	}



}
