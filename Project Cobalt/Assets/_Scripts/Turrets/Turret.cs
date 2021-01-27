using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IDestructible
{

	float health = 5;

	public event DamagedEvent OnDamaged;
	public event DestroyedEvent OnDestroy;

	protected virtual void Initialisation() {
		OnDestroy += Destroy;
	}

	// Start is called before the first frame update
	void Start()
    {
		Initialisation();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public bool Targetable(Team team) {
		return true;
	}

	public void Damage(float amount) {
		health = Mathf.Clamp(health - amount, 0, 5);
		OnDamaged?.Invoke(health, amount);
		if (health <= 0)
			OnDestroy?.Invoke();
	}

	protected void Destroy() {
		Destroy(gameObject);
	}


}
