using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class WeaponSelectOption : MenuOption
{
	
	[SerializeField] WeaponType weaponType = WeaponType.Light;

	Weapon[] weapons;
	Text nameText;
	
	public override void Select() {
		ChangeWeapon(WeaponSelection.instance.choosenWeapons[(int)weaponType], 1);
	}

	void Start() {
		nameText = GetComponentInChildren<Text>();
		switch (weaponType) {
			case WeaponType.Light:
				weapons = Resources.LoadAll<Weapon>("PlayerMechWeapons/LightWeapons");
				break;
			case WeaponType.Heavy:
				weapons = Resources.LoadAll<Weapon>("PlayerMechWeapons/HeavyWeapons");
				break;
			case WeaponType.Artillery:
				weapons = Resources.LoadAll<Weapon>("PlayerMechWeapons/ArtilleryWeapons");
				break;
		}
		ChangeDisplay(WeaponSelection.instance.choosenWeapons[(int)weaponType]);
	}

	public void ChangeWeapon(Weapon current, int changeIndex) {
		int index = 0;
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons[i].Equals(current)) {
				index = i;
				break;
			}
		}
		index += changeIndex;
		if (index >= weapons.Length)
			index = 0;
		else if (index < 0)
			index = weapons.Length - 1;

		if (WeaponSelection.instance) {
			WeaponSelection.instance.choosenWeapons[(int)weaponType] = weapons[index];
			ChangeDisplay(WeaponSelection.instance.choosenWeapons[(int)weaponType]);
		}
	}

	void ChangeDisplay(Weapon selectedWeapon) {
		nameText.text = selectedWeapon.ConfigFile.Name;
	}

}
