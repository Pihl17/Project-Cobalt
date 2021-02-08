using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public abstract class Turret<T> : MonoBehaviour, IDestructible where T : Weapon
{

	protected T gun;
	public WeaponConfig weaponConfig;

	public Transform target;
	protected Vector3 toTarget;

	float health = 5;

	public event DamagedEvent OnDamaged;
	public event DestroyedEvent OnDestroy;

	protected virtual void Initialisation() {
		OnDestroy += Destroy;
		gun.SetConfigFile(weaponConfig);
	}

	// Start is called before the first frame update
	void Start()
    {
		Initialisation();
	}

    // Update is called once per frame
    void Update()
    {
		gun.UpdateCooldown();
		Aim();
    }

	public bool Targetable(Team team) {
		return true;
	}

	public void Damage(float amount) {
		health = Mathf.Clamp(health - amount, 0, 5);
		OnDamaged?.Invoke(health, amount);
		if (health <= 0)
			OnDestroy?.Invoke();
	}

	protected void Destroy() {
		Destroy(gameObject);
	}

	void Aim() {
		toTarget = target.position - transform.position;
		FireGun();
	}

	protected abstract void FireGun();

}
