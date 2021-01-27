using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class MachineGunTurret : Turret {

	MachineGun gun;
	public WeaponConfig weaponConfig;

	public Transform target;
	Vector3 toTarget;

	protected override void Initialisation() {
		base.Initialisation();
		gun = new MachineGun();
		gun.SetConfigFile(weaponConfig);
		gun.InitialisateEffects(transform);
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
		toTarget = target.position - transform.position;
		gun.Fire(transform.position + toTarget.normalized * 0.5f, toTarget);
    }
}
