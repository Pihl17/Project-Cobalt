using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretScript : EnemyScript
{

	public GameObject bullets;
	//float fireRate = 0.8f;
	float fireTimer;
	public Transform turningPoint;

	void Update() {
		if (AwareOfPlayer()) {
			turningPoint.LookAt(turningPoint.position + Vector3.RotateTowards(turningPoint.forward, target.position - turningPoint.position, Mathf.PI * Time.deltaTime, Time.deltaTime), Vector3.up);
			fireTimer += Time.deltaTime;
			if (fireTimer >= 1 / configFile.FireRate) {
				GameObject bullet = Instantiate(bullets, turningPoint.position + turningPoint.forward * 1.5f, turningPoint.rotation);
				bullet.GetComponent<BulletScript>().Fire(turningPoint.forward * 10, configFile.Damage);
				fireTimer = 0;
			}
		}

	}


}
