using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MechConfig", menuName = "ScriptableObject/MechConfig", order = 3)]
public class MechConfig : ScriptableObject
{
	[SerializeField] Team team = Team.Blue;
	public Team Team { get { return team; } }
	[SerializeField] float maxHealth = 10;
	public float MaxHealth { get { return maxHealth; } }
	[SerializeField] float moveSpeed = 5;
	public float MoveSpeed { get { return moveSpeed; } }
	[SerializeField] float maxMoveSpeed = 5;
	public float MaxMoveSpeed { get { return maxMoveSpeed; } }
	[SerializeField] float turnSpeed = 45;
	public float TurnSpeed { get { return turnSpeed; } }
	[SerializeField] float lockOnDistance = 15;
	public float LockOnDistrance { get { return lockOnDistance; } }


	[SerializeField] Vector3 gunLocation = new Vector3();
	public Vector3 GunLocation { get { return gunLocation; } }
	[SerializeField] Vector3 launcherLocation = new Vector3();
	public Vector3 LauncherLocation { get { return launcherLocation; } }

}
