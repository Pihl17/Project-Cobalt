using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Human : MonoBehaviour, IDestructible
{

	public RiflemanConfig config;

	float health;
	Rigidbody rig;

	public event HealthChangeEvent OnHealthChanged;
	public event DestroyedEvent OnDestroy;

	protected virtual void Initialise() {
		health = config.MaxHealth;
		OnDestroy += Destroy;
		rig = GetComponent<Rigidbody>();
	}
	
	// Start is called before the first frame update
    void Start()
    {
		Initialise();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Damage(float amount) {
		health = Mathf.Clamp(health - amount, 0, config.MaxHealth);
		OnHealthChanged?.Invoke(health, amount);
		if (health <= 0)
			OnDestroy?.Invoke();
	}

	protected void Destroy() {
		Destroy(gameObject);
	}

	protected void Move(Vector3 dir) {
		rig.MovePosition(transform.position + dir.normalized * config.MoveSpeed * Time.fixedDeltaTime);
	}

}
