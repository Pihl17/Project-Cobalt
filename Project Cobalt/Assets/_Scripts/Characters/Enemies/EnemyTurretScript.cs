using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretScript : EnemyScript
{
    
	public GameObject bullets;
	float fireRate = 0.8f;
	float fireTimer;
	public Transform turningPoint;

	// Start is called before the first frame update
    void Start()
    {
		Initialization();
	}

    // Update is called once per frame
    void Update()
    {
		if (AwareOfPlayer()) {
			turningPoint.LookAt(turningPoint.position + Vector3.RotateTowards(turningPoint.forward, target.position - turningPoint.position, Mathf.PI * Time.deltaTime, Time.deltaTime), Vector3.up);
			fireTimer += Time.deltaTime;
			if (fireTimer >= 1/fireRate) {
				Instantiate(bullets, turningPoint.position + turningPoint.forward * 1.5f, turningPoint.rotation);
				fireTimer = 0;
			}
		}

    }



}
