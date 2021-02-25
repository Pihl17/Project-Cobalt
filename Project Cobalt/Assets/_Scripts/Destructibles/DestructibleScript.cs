using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleScript : MonoBehaviour, IDestructible {

	public DestructibleConfig configFile;
	float health = 10;

	public event DamagedEvent OnDamaged;
	public event DestroyedEvent OnDestroy;

	protected virtual void Initialisation() {
		health = configFile.MaxHealth;
		OnDestroy += Destroy;
	}

	void Start() {
		Initialisation();
	}

	public void Damage(float amount) {
		health -= amount;
		if (OnDamaged != null)
			OnDamaged.Invoke(health, amount);
		if (health <= 0) {
			Die();
		}

	}

	protected void Die() {
		if (OnDestroy != null)
			OnDestroy.Invoke();
	}

	protected virtual void Destroy() {
		GameObject.Destroy(gameObject);
	}

}
