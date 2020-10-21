using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterScript : MonoBehaviour
{
    
	//Stats
	float moveSpeed = 2.5f;
	protected float health = 10;
	protected float maxShield = 4;
	protected float shield = 0;

	//Components
	Rigidbody rig;

	protected virtual void Initialization() {
		rig = GetComponent<Rigidbody>();
	}

	public void Move(Vector3 moveDirection) {
		rig.AddForce(moveDirection * moveSpeed, ForceMode.VelocityChange);
		if (rig.velocity.magnitude > moveSpeed) // TODO: This part about keeping a max speed causes the character to fall slowly as long as they are moving... Need to keep the y coordinate out of it somehow...
			rig.velocity = rig.velocity.normalized * moveSpeed;
	}

	public virtual void Damage(float amount) {
		if (shield > 0) {
			shield -= amount;
			if (shield < 0) {
				health += shield;
				shield = 0;
			}
		} else
			health -= amount;
		if (health <= 0)
			health = 0;
	}
		
	public virtual void GainShield(int amount) {
		shield += amount;
		if (shield > maxShield)
			shield = maxShield;
	}

}
