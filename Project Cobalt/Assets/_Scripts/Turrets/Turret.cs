using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using UnityEditor;

public class Turret : MonoBehaviour, IDestructible
{
	
	Weapon gun;
	public Weapon.WeaponType weaponType;
	public WeaponConfig weaponConfig;

	//public Transform target;
	WeaponFireContext fireContext;

	public float maxHealth = 5;
	float health;

	public event HealthChangeEvent OnHealthChanged;
	public event DestroyedEvent OnDestroy;
	Transform targetInRange;

	protected virtual void Initialisation() {
		gun = Weapon.DefineType(weaponType);
		fireContext = new WeaponFireContext();
		fireContext.userTrans = transform;
		FindTarget();
		OnDestroy += Destroy;
		gun.SetConfigFile(weaponConfig);
		health = maxHealth;
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

	public void Damage(float amount) {
		health = Mathf.Clamp(health - amount, 0, 5);
		OnHealthChanged?.Invoke(health, amount);
		if (health <= 0)
			OnDestroy?.Invoke();
	}

	protected void Destroy() {
		Destroy(gameObject);
	}

	public void AddTarget(PlayerControl target) {
		targetInRange = target.transform;
	}

	public void RemoveTarget(PlayerControl target) {
		if (targetInRange == target.transform)
			targetInRange = null;
	}

	public WeaponConfig GetWeaponConfig() {
		return weaponConfig;
	}

	void Aim() {
		if (targetInRange) {
			fireContext.targetVector = targetInRange.position - transform.position;
			fireContext.firePos = fireContext.targetVector.normalized * 0.75f;
			FireGun();
		}
	}

	void FindTarget() {
		fireContext.target = targetInRange;
	}

	void FireGun() {
		gun.Fire(fireContext);
	}

}
