using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObject/EnemyConfig", order = 1)]
public class EnemyConfig : ScriptableObject
{

	[SerializeField] float maxHealth = 0;
	public float MaxHealth { get{ return maxHealth; } }

	[SerializeField] float awarenessRadius = 10;
	public float AwarenessRadius { get{ return awarenessRadius; } }

	/*void STOP_THIS_NONSENSE() {
		maxHealth = 0;
	}*/

}
