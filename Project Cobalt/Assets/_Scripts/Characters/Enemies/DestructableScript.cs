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
	//public UnityEvent OnDestroyEvent;

	protected virtual void Initialization() {
		health = configFile.MaxHealth;
		OnDestroy += Destroy;
	}

	void Start() {
		Initialization();
	}

	public void Damage(float amount) {
		//print("Enemy received " + amount + " points of damage!");
		health -= amount;
		if (health <= 0) {
			//OnDestroyEvent.Invoke();
			if (OnDestroy != null)
				OnDestroy();
		}
	}

	protected virtual void Destroy() {
		GameObject.Destroy(gameObject);
	}

}
