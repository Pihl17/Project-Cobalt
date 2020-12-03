using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObject/EnemyConfig", order = 2)]
public class EnemyConfig : ScriptableObject {
	
	[SerializeField] float maxHealth = 10;
	public float MaxHealth { get { return maxHealth; } }

	[SerializeField] float moveSpeed = 5;
	public float MoveSpeed { get { return moveSpeed; } }

	[SerializeField] float awarenessRadius = 10;
	public float AwarenessRadius { get{ return awarenessRadius; } }

	[SerializeField] float damage = 1;
	public float Damage { get { return damage; } }

	[SerializeField] float fireRate = 1;
	public float FireRate { get { return fireRate; } }

	[SerializeField] float attackRange = 10;
	public float AttackRange { get { return attackRange; } }

}
