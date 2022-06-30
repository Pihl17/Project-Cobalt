using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

[CreateAssetMenu(fileName = "Turret Config", menuName = "ScriptableObject/Turret Config", order = 5)]
public class TurretConfig : ScriptableObject
{

	[SerializeField] float maxHealth = 5;
	public float MaxHealth { get { return maxHealth; } }
	[SerializeField] WeaponConfig weaponConfig = null;
	public WeaponConfig WeaponConfig { get { return weaponConfig; } }

}
