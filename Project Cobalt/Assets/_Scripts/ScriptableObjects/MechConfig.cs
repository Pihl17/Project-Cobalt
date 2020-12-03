using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MechConfig", menuName = "ScriptableObject/MechConfig", order = 3)]
public class MechConfig : ScriptableObject
{

	[SerializeField] float maxHealth = 10;
	public float MaxHealth { get { return maxHealth; } }
	[SerializeField] float moveSpeed = 5;
	public float MoveSpeed { get { return moveSpeed; } }
	[SerializeField] float lockOnDistance = 15;
	public float LockOnDistrance { get { return lockOnDistance; } }

}
