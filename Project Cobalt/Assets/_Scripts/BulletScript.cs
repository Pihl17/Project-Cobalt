﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BulletScript : MonoBehaviour
{
	public bool enemyBullet;

	float timer = 0;
	protected float timeAlive = 3;
	protected float damage = 2;

	protected Rigidbody rig;

	public delegate void ImpactEvent(Collision collision);
	public event ImpactEvent OnImpact;


	public void Fire(Vector3 velocity, float _damage) {
		Initialization();
		damage = _damage;
		rig.velocity = velocity;
	}

	protected virtual void Initialization() {
		rig = GetComponent<Rigidbody>();
		OnImpact += DamageCollision;
	}

    // Update is called once per frame
    void Update()
    {
		CheckForSelfDestruct();
    }

	protected void CheckForSelfDestruct() {
		timer += Time.deltaTime;
		if (timer > timeAlive)
			GameObject.Destroy(gameObject);
	}


	public virtual void OnCollisionEnter(Collision col) {
		if (OnImpact != null)
			OnImpact.Invoke(col);
		GameObject.Destroy(gameObject);
	}

	protected bool DamageCollision(Collider col) {
		if (enemyBullet && col.gameObject.GetComponent<PlayerMechControllerScript>()) {
			col.gameObject.GetComponent<PlayerMechControllerScript>().Damage(damage);
			return true;
		} else if (!enemyBullet && col.gameObject.GetComponent<IDamageable>() != null && !col.gameObject.GetComponent<PlayerMechControllerScript>()) {
			col.gameObject.GetComponent<IDamageable>().Damage(damage);
			return true;
		}
		return false;
	}

	protected void DamageCollision(Collision col) {
		DamageCollision(col.collider);
	}


}
