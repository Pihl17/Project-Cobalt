using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using UnityEditor;


public class Turret : MonoBehaviour, IDestructible, IDetectingUnit
{

	public TurretConfig config;

	//public Weapon.WeaponType weaponType;
	//public WeaponConfig weaponConfig;
	//public float maxHealth = 5;

	float health;
	Weapon gun;
	WeaponFireContext fireContext;
	Transform targetInRange;

	public event HealthChangeEvent OnHealthChanged;
	public event DestroyedEvent OnDestroy;

	protected virtual void Initialisation() {
		gun = Weapon.DefineType(config.WeaponType, config.WeaponConfig);
		fireContext = new WeaponFireContext();
		fireContext.userTrans = transform;
		FindTarget();
		OnDestroy += Destroy;
		health = config.MaxHealth;
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
		health = Mathf.Clamp(health - amount, 0, config.MaxHealth);
		OnHealthChanged?.Invoke(health, amount);
		if (health <= 0)
			OnDestroy?.Invoke();
	}

	protected void Destroy() {
		Destroy(gameObject);
	}

	public void AddTarget(Transform target) {
		targetInRange = target.transform;
	}

	public void RemoveTarget(Transform target) {
		if (targetInRange == target.transform)
			targetInRange = null;
	}

	public float GetDetectionRange() {
		return config.WeaponConfig.Range;
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
