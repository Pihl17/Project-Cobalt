using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuOption : MenuOption
{

	public GameObject toCanvas;
	MenuManager menuManager;

	void Start() {
		menuManager = GetComponentInParent<MenuManager>();
	}

	public override void Select() {
		menuManager.ChangeMenu(toCanvas);
	}
}
