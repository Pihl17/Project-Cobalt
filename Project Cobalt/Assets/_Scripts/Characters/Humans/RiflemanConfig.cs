using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

[CreateAssetMenu(fileName = "Rifleman Config", menuName = "ScriptableObject/Rifleman Config", order = 4)]
public class RiflemanConfig : ScriptableObject
{

	[SerializeField] float maxHealth = 1;
	public float MaxHealth { get { return maxHealth; } }
	[SerializeField] float moveSpeed = 2;
	public float MoveSpeed { get { return moveSpeed; } }
	[SerializeField] WeaponConfig weaponConfig = null;
	public WeaponConfig WeaponConfig { get { return weaponConfig; } }
	[SerializeField] float detectionRange = 10;
	public float DetectionRange { get { return detectionRange; } }

}
