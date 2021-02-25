using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructible 
{
	event DamagedEvent OnDamaged;
	event DestroyedEvent OnDestroy;

	void Damage(float amount);

}

public delegate void DamagedEvent(float remainingHealth, float healthLost);
public delegate void DestroyedEvent();

