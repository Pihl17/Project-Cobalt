using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Vehicle : MonoBehaviour, IDestructible
{

	public event HealthChangeEvent OnHealthChanged;
	public event DestroyedEvent OnDestroy;

	protected Rigidbody rig;
	float health = 10;
	public float Health { get { return health; } }
	Vector3 speed;

	protected abstract float GetMaxVel();
	public abstract float GetMaxHealth();

	protected virtual void Initialisation() {
		health = GetMaxHealth();
		rig = GetComponent<Rigidbody>();
	}

	public virtual void Damage(float amount) {
		health = Mathf.Clamp(health - amount, 0, GetMaxHealth());
		OnHealthChanged?.Invoke(health, amount);
		if (health <= 0)
			Destroy();
	}

	public virtual void Heal(float amount) {
		health = Mathf.Clamp(health + amount, 0, GetMaxHealth());
		OnHealthChanged?.Invoke(health, -amount);
	}


	protected virtual void Drive(float forward) {
		rig.AddForce(transform.forward * forward);
		LimitMoveSpeed();
	}
	
	protected void LimitMoveSpeed() {
		speed = rig.velocity;
		speed.y = 0;
		if (speed.magnitude > GetMaxVel()) {
			rig.velocity = Vector3.up * rig.velocity.y + speed.normalized * GetMaxVel();
		}
	}

	public virtual void Turn(float rightAngle) {
		transform.Rotate(Vector3.up * rightAngle * Time.deltaTime);
	}

	protected virtual void Destroy() {
		OnDestroy?.Invoke();
	}


}
