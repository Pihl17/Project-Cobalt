using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : PickUp
{

	protected override void PickUpEffect(CombatMech mech) {
		mech.Heal(mech.GetMaxHealth());
	}

}
