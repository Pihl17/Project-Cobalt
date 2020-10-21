using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBulletScript : BulletScript
{

	float explodeRadius = 1;

	public void Fire(Vector3 velocity, float _damage, float _explodeRadius) {
		Initialization();
		damage = _damage;
		explodeRadius = _explodeRadius;
		rig.velocity = velocity;
	}

	protected override void Initialization() {
		base.Initialization();
		timeAlive = 10;
	}

	// Start is called before the first frame update
    void Start()
    {
		//Initialization();
    }

    // Update is called once per frame
    void Update()
    {
		checkForSelfDestruct();
    }
		
	public override void OnCollisionEnter(Collision col) {
		Debug.DrawRay(transform.position, Vector3.right * explodeRadius, Color.red, 2.0f);
		Debug.DrawRay(transform.position, -Vector3.right * explodeRadius, Color.red, 2.0f);
		Debug.DrawRay(transform.position, Vector3.forward * explodeRadius, Color.red, 2.0f);
		Debug.DrawRay(transform.position, -Vector3.forward * explodeRadius, Color.red, 2.0f);
		Debug.DrawRay(transform.position, Vector3.up * explodeRadius, Color.red, 2.0f);
		Debug.DrawRay(transform.position, -Vector3.up * explodeRadius, Color.red, 2.0f);

		Collider[] colInRange = Physics.OverlapSphere(transform.position, explodeRadius);
		for (int i = 0; i < colInRange.Length; i++) {
			DamageCollision(colInRange[i]);
		}
		GameObject.Destroy(gameObject);
	}


}
