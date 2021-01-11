using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(ShowMenuOptionConnections))]
public class MenuOption : MonoBehaviour
{

	[SerializeField] string optionName = "";

	public MenuOption[] nearbourOption = new MenuOption[System.Enum.GetValues(typeof(Direction)).Length];

	public void Select() {
		print(optionName + " has been selected");
	}

	public enum Direction { Up, Right, Down, Left }
	public MenuOption GetNearbour(Direction direction) {
		return nearbourOption[(int)direction];
	}

}
