using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPauseOption : MenuOption
{

	PauseMenu menu;

	private void Start() {
		menu = GetComponentInParent<PauseMenu>();
	}

	public override void Select() {
		menu.Pause(false);
	}
}
