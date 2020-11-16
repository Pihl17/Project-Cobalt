using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenuOption : MenuOption
{

	public override void Select() {
		// TODO: Close down the menu again and find the player object so that the robot controls can be activated again
		//GameObject.Find("PlayerController").GetComponent<PlayerControlScript>().SwitchToInGameUI(true);
	}

}
