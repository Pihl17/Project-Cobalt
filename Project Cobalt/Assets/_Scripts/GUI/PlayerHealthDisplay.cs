using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerHealthDisplay : MonoBehaviour
{

	Text healthText;

	void Awake() {
		healthText = GetComponent<Text>();
	}

	void UpdateHealthDisplay(float health) {
		healthText.text = "Health: " + health.ToString("F0");
	}

	void UpdateHealthDisplay(CombatMech playerScript) {
		UpdateHealthDisplay(playerScript.Health);
	}
	
	void OnEnable() {
		PlayerControl.OnPlayerHealthChange += UpdateHealthDisplay;
		PlayerControl.OnPlayerInitialisation += UpdateHealthDisplay;
	}

	void OnDisable() {
		PlayerControl.OnPlayerHealthChange -= UpdateHealthDisplay;
		PlayerControl.OnPlayerInitialisation -= UpdateHealthDisplay;
	}

}
