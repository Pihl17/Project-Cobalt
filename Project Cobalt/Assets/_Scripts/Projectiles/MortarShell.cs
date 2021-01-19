using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShell : ExplosiveProjectile
{

	protected override void Initialization() {
		base.Initialization();
		timeAlive = 10;
	}

    // Update is called once per frame
    void Update()
    {
		CheckForSelfDestruct();
    }

}
