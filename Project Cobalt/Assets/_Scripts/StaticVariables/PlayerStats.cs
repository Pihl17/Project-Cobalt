using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;

public static class PlayerStats
{
    
	public static List<Ability> abilityInv = new List<Ability>();

	public static Ability GetLastAbilityInInv() {
		return abilityInv[abilityInv.Count - 1];
	}

	public static Ability ReplaceAbility(int index, Ability replacement) {
		Ability temp = abilityInv[index];
		abilityInv[index] = replacement;
		return temp;
	}

	public static void RotateAbilityInv(bool pushPositiveDir) {
		int dir = pushPositiveDir ? 1 : -1;
		int currentPos = pushPositiveDir ? 0 : abilityInv.Count - 1;
		Ability temp = abilityInv[pushPositiveDir ? abilityInv.Count - 1 : 0];
		for (int i = 0; i < abilityInv.Count; i++) {
			temp = ReplaceAbility(currentPos + dir*i, temp);
		}

	}

}
