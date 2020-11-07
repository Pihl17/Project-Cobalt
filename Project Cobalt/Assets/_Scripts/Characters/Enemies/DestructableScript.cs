using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;

public class DestructableScript : MonoBehaviour
{

	public EnemyConfig configFile;
	float health = 10;

	public delegate void DestroyedEvent();
	public DestroyedEvent OnDestroy;

	protected virtual void Initialization() {
		health = configFile.MaxHealth;
		OnDestroy += Destroy;
	}

	void Start() {
		Initialization();
	}

	public void Damage(float amount) {
		health -= amount;
		if (health <= 0) {
			Die();
		}
	}

	protected void Die() {
		if (OnDestroy != null)
			OnDestroy();
	}

	protected virtual void Destroy() {
		GameObject.Destroy(gameObject);
	}

}
