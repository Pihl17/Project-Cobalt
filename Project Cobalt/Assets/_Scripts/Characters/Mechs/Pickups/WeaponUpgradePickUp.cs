using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradePickUp : PickUp
{

	enum WeapnType { Gun, Heavy, Artillery}
	[SerializeField] WeapnType upgradeWeaponType = WeapnType.Gun;

	protected override void PickUpEffect(CombatMech mech) {
		if (mech.Weapons.Length > (int)upgradeWeaponType)
			mech.Weapons[(int)upgradeWeaponType].Upgrade();
	}

}
