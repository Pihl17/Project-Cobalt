using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class PlayerAmmoDisplay : MonoBehaviour
{

	Weapon[] weapons;
	public Text[] ammoTexts;

	public void SetWeaponsToTrack(params Weapon[] _weapons) {
		weapons = _weapons;
		UpdateDisplays();
	}

	void LateUpdate() {
		UpdateDisplays();
	}

	void UpdateDisplays() {
		if (weapons.Length == 0 || ammoTexts.Length == 0)
			return;

		for (int i = 0; i < weapons.Length; i++) {
			ammoTexts[i].text = weapons[i].Ammo.ToString();
		}
	}

}
