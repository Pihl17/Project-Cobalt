using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObject/EnemyConfig", order = 1)]
public class EnemyConfig : ScriptableObject
{

	[SerializeField] float maxHealth;
	public float MaxHealth { get{ return maxHealth; } }


	void STOP_THIS_NONSENSE() {
		maxHealth = 0;
	}

}
