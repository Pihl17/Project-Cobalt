using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : RobotGUIDisplayScript
{
    


	protected override void Initialization() {
		base.Initialization();
	}

	void Start() {
		Initialization();
	}

	void Update() {
		FindLockOnTarget();
	}

}
