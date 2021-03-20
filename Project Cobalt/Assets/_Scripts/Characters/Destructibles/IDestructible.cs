using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructible 
{
	event HealthChangeEvent OnHealthChanged;
	event DestroyedEvent OnDestroy;

	void Damage(float amount);

}

public delegate void HealthChangeEvent(float remainingHealth, float healthLost);
public delegate void DestroyedEvent();

