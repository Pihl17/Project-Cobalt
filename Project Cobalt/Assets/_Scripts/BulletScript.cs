using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletScript : MonoBehaviour
{
	public bool enemyBullet;

	float timer = 0;
	protected float timeAlive = 3;
	protected float speed = 10;
	protected float damage = 2;

	protected Rigidbody rig;

	protected virtual void Initialization() {
		rig = GetComponent<Rigidbody>();
	}

	// Start is called before the first frame update
    void Start()
    {
		Initialization();
		rig.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
		checkForSelfDestruct();
    }

	protected void checkForSelfDestruct() {
		timer += Time.deltaTime;
		if (timer > timeAlive)
			GameObject.Destroy(gameObject);
	}


	public virtual void OnCollisionEnter(Collision col) {
		if (DamageCollision(col.collider))
			GameObject.Destroy(gameObject);
	}

	protected bool DamageCollision(Collider col) {
		if (enemyBullet && col.gameObject.GetComponent<RobotScript>()) {
			col.gameObject.GetComponent<RobotScript>().Damage(damage);
			return true;
		} else if (!enemyBullet && col.gameObject.GetComponent<DestructableScript>()) {
			col.gameObject.GetComponent<DestructableScript>().Damage(damage);
			return true;
		}
		return false;
	}

}
