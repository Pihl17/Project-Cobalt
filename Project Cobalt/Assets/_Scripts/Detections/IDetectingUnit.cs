using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using System;

public interface IDetectingUnit 
{

	WeaponConfig GetWeaponConfig();

	void AddTarget(Transform target);
	void RemoveTarget(Transform target);

}
