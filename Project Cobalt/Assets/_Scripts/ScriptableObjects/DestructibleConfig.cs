using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestructibleConfig", menuName = "ScriptableObject/DestructibleConfig", order = 1)]
public class DestructibleConfig : ScriptableObject {

	[SerializeField] float maxHealth = 10;
	public float MaxHealth { get { return maxHealth; } }

}
