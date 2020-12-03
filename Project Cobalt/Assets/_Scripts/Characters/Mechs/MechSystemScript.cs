using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MechSystemScript : MonoBehaviour, IDestructible {

	public MechConfig configFile;
	float health;
	protected Rigidbody rig;
	protected Transform lockOnTarget;

	float rotateSpeed = 45f;

	public event DamagedEvent OnDamaged;
	public event DestroyedEvent OnDestroy;

	protected virtual void Initialisation() {
		rig = GetComponent<Rigidbody>();
		health = configFile.MaxHealth;
	}

	void Start() {
		Initialisation();
	}

	void FixedUpdate() {
		
	}

	public void Damage(float amount) {
		health = Mathf.Clamp(health - amount, 0, configFile.MaxHealth);
		if (OnDamaged != null)
			OnDamaged.Invoke(health, amount);
		if (health <= 0)
			Destroy();
	}

	protected virtual void Destroy() {
		if (OnDestroy != null)
			OnDestroy.Invoke();
	}

	protected void Move(Vector3 moveDirection) {
		rig.AddForce(moveDirection * configFile.MoveSpeed, ForceMode.VelocityChange);
		//rig.velocity = moveDirection * moveSpeed;
		LimitMoveVelocity();
	}

	protected void LimitMoveVelocity() {
		if (rig.velocity.magnitude > configFile.MoveSpeed) // TODO: This part about keeping a max speed causes the character to fall slowly as long as they are moving... Need to keep the y coordinate out of it somehow...
			rig.velocity = rig.velocity.normalized * configFile.MoveSpeed;
	}

	protected void Turn(float rightAngle) {
		transform.Rotate(Vector3.up * rightAngle * rotateSpeed * Time.deltaTime);
	}

	protected void FindLockOnTarget() {
		lockOnTarget = null;
		Collider[] possibleTargets = Physics.OverlapBox(transform.position + transform.forward * (configFile.LockOnDistrance / 2 + 1f), Vector3.one * configFile.LockOnDistrance / 2, transform.rotation);
		float tempAngle;
		float tempMinAngle = 90;
		for (int i = 0; i < possibleTargets.Length; i++) {
			if (possibleTargets[i].GetComponent<IDestructible>() != null && (possibleTargets[i].transform.position - transform.position).magnitude <= configFile.LockOnDistrance) {
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
