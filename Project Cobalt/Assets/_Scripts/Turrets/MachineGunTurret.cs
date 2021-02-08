﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class MachineGunTurret : Turret<MachineGun> {

	protected override void Initialisation() {
		gun = new MachineGun();
		base.Initialisation();
		gun.InitialisateEffects(transform);
	}

	protected override void FireGun() {
		gun.Fire(transform.position + toTarget.normalized * 0.5f, toTarget);
	}
}
