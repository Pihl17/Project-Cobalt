using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObject/PlayerConfig", order = 3)]
public class PlayerConfig : CharacterConfig {

	[SerializeField] float lockOnDistance = 15;
	public float LockOnDistrance { get { return lockOnDistance; } }

}
