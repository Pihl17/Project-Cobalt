using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityChoiceMenuOption : MenuOption
{
	public int abilityNumber;
	Text text;

	void Start() {
		text = GetComponent<Text>();
		UpdateAbilityDisplay();
	}

	public override MenuOption GetUpNeighbour() {
		ChangeAbility(true);
		return null;
	}

	public override MenuOption GetDownNeighbour() {
		ChangeAbility(false);
		return null;
	}

	void ChangeAbility(bool positiveDir) {
		/*RobotScript robot = GameObject.Find("PlayerController").GetComponent<PlayerControlScript>().currentRobot;
		robot.SwitchAbility(abilityNumber, PlayerStats.ReplaceAbility(positiveDir ? 0 : (PlayerStats.abilityInv.Count-1), robot.GetAbility(abilityNumber)));
		PlayerStats.RotateAbilityInv(!positiveDir);
		UpdateAbilityDisplay();*/
	}

	void UpdateAbilityDisplay() {
		/*if (text)
			text.text = GameObject.Find("PlayerController").GetComponent<PlayerControlScript>().currentRobot.GetAbility(abilityNumber).GetName();*/
	}

	public override void Select() {
		
	}

}
