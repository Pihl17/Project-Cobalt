using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class FlamethrowerTurret : Turret<Flamethrower>
{

	protected override void Initialisation() {
		gun = new Flamethrower();
		base.Initialisation();
	}

	protected override void FireGun() {
		gun.Fire(toTarget);
	}

}
