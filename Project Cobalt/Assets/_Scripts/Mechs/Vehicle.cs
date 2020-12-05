using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Vehicle : MonoBehaviour, IDestructible
{

	public event DamagedEvent OnDamaged;
	public event DestroyedEvent OnDestroy;

	protected Rigidbody rig;
	float health = 10;
	Vector3 speed;

	protected abstract float GetMaxVel();
	protected abstract float GetMaxHealth();


	public void Damage(float amount) {
		health = Mathf.Max(health - amount, 0);
		if (OnDamaged != null)
			OnDamaged.Invoke(health, amount);
		if (health <= 0)
			Destroy();
	}

	protected virtual void Initialisation() {
		health = GetMaxHealth();
		rig = GetComponent<Rigidbody>();
	}

	protected virtual void Drive(float forward) {
		rig.AddForce(transform.forward * forward);
		LimitSpeed();
	}
	
	protected void LimitSpeed() {
		speed = rig.velocity;
		speed.y = 0;
		if (speed.magnitude > GetMaxVel()) {
			rig.velocity = Vector3.up * rig.velocity.y + speed.normalized * GetMaxVel();
		}
	}

	protected virtual void Turn(float rightAngle) {
		transform.Rotate(Vector3.up * rightAngle * Time.deltaTime);
	}

	protected virtual void Destroy() {
		if (OnDestroy != null)
			OnDestroy.Invoke();
	}



}
