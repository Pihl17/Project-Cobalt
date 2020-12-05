using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public static class PlayerStats
{
    
	public static List<Weapon> abilityInv = new List<Weapon>();

	public static Weapon GetLastAbilityInInv() {
		return abilityInv[abilityInv.Count - 1];
	}

	public static Weapon ReplaceAbility(int index, Weapon replacement) {
		Weapon temp = abilityInv[index];
		abilityInv[index] = replacement;
		return temp;
	}

	public static void RotateAbilityInv(bool pushPositiveDir) {
		int dir = pushPositiveDir ? 1 : -1;
		int currentPos = pushPositiveDir ? 0 : abilityInv.Count - 1;
		Weapon temp = abilityInv[pushPositiveDir ? abilityInv.Count - 1 : 0];
		for (int i = 0; i < abilityInv.Count; i++) {
			temp = ReplaceAbility(currentPos + dir*i, temp);
		}

	}

}
