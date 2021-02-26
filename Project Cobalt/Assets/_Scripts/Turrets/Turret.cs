using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class Turret : MonoBehaviour, IDestructible
{

	Weapon gun;
	public WeaponConfig weaponConfig;

	public Transform target;
	WeaponFireContext fireContext;

	float health = 5;

	public event HealthChangeEvent OnHealthChanged;
	public event DestroyedEvent OnDestroy;

	protected virtual void Initialisation() {
		gun = new MachineGun();
		fireContext = new WeaponFireContext();
		fireContext.userTrans = transform;
		FindTarget();
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

	public void Damage(float amount) {
		health = Mathf.Clamp(health - amount, 0, 5);
		OnHealthChanged?.Invoke(health, amount);
		if (health <= 0)
			OnDestroy?.Invoke();
	}

	protected void Destroy() {
		Destroy(gameObject);
	}

	void Aim() {
		fireContext.targetVector = target.position - transform.position;
		fireContext.firePos = fireContext.targetVector.normalized * 0.5f;
		FireGun();
	}

	void FindTarget() {
		fireContext.target = target;
	}

	void FireGun() {
		gun.Fire(fireContext);
	}

}
