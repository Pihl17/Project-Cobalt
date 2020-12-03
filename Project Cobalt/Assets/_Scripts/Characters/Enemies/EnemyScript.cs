using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDestructible {

	public EnemyConfig configFile;
	PlayerMechControllerScript playerScript;
	protected Transform target;
	bool awareOfPlayer = false;

	public event DamagedEvent OnDamaged;
	public event DestroyedEvent OnDestroy;

	float health;

	public void Damage(float amount) {
		health = Mathf.Clamp(health - amount, 0, configFile.MaxHealth);
		if (OnDamaged != null)
			OnDamaged.Invoke(health, amount);
		if (health <= 0) {
			Die();
		}
	}

	protected virtual void Initialisation() {
		playerScript = GameObject.Find("PlayerMech").GetComponent<PlayerMechControllerScript>();
		health = configFile.MaxHealth;
		OnDestroy += Destroy;
	}

	void Start() {
		Initialisation();
	}

	protected bool AwareOfPlayer() {
		target = playerScript.transform;
		if (target) {
			if (awareOfPlayer)
				return true;
			else if ((target.position - transform.position).magnitude < configFile.AwarenessRadius)
				awareOfPlayer = true;
		}
		return false;
	}

	protected void Die() {
		OnDestroy?.Invoke();
	}

	void Destroy() {
		GameObject.Destroy(gameObject);
	}

}
