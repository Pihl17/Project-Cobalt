using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableScript : MonoBehaviour
{

	public EnemyConfig configFile;

	float health = 10;

	protected virtual void Initialization() {
		health = configFile.MaxHealth;
	}

	void Start() {
		Initialization();
	}

	public void Damage(float amount) {
		//print("Enemy received " + amount + " points of damage!");
		health -= amount;
		if (health <= 0)
			GameObject.Destroy(gameObject);
	}
		
}
