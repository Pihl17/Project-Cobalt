using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretConfig", menuName = "ScriptableObject/TurretConfig", order = 5)]
public class TurretConfig : ScriptableObject {

	[SerializeField] float maxHealth = 10;
	public float MaxHealth { get { return maxHealth; } }
	[SerializeField] float turnSpeed = 45f;
	public float TurnSpeed { get { return turnSpeed; } }

	[Header("Damage")]
	[SerializeField] float firePower = 1;
	public float FirePower { get { return firePower; } }
	[SerializeField] float fireRate = 1;
	public float FireRate { get { return fireRate; } }
	[SerializeField] float fireRange = 10;
	public float FireRange { get { return fireRange; } }

	[Header("Precinct Assault")]
	[SerializeField] float destroyedTime = 1f;
	public float DestroyedTime { get { return destroyedTime; } }
	[SerializeField] int destroyReward = 0;
	public int DestroyReward { get { return destroyReward; } }
	[SerializeField] int captureReward = 0;
	public int CaptureReward { get { return captureReward; } }
}
