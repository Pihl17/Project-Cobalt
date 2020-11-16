using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterConfig : DestructibleConfig {

	[SerializeField] float moveSpeed = 5;
	public float MoveSpeed { get { return moveSpeed; } }

}
