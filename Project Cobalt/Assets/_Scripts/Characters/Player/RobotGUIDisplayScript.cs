using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotGUIDisplayScript : RobotAbilityUseScript
{

	public Text healthText;
	public Text shieldText;

	protected override void Initialization() {
		base.Initialization();
		UpdateHealthDisplay();
	}

	public override void Damage(float amount) {
		base.Damage(amount);
		UpdateHealthDisplay();
	}

	void UpdateHealthDisplay() {
		if (healthText)
			healthText.text = health.ToString();
		if (shieldText)
			shieldText.text = shield.ToString();
	}

	public override void GainShield(int amount) {
		base.GainShield(amount);
		UpdateHealthDisplay();
	}

}
