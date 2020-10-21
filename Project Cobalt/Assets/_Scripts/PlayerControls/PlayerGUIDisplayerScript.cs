using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Abilities;

public class PlayerGUIDisplayerScript : MonoBehaviour
{
    
	public Text[] abilityCooldownText;

	public GameObject inGameUI;
	public GameObject choseAbilityUI;
	protected MenuManagementScript currentUI;

	protected void UpdateAbilityCooldownDisplay(params Ability[] ability) {
		for (int i = 0; i < ability.Length && i < abilityCooldownText.Length; i++) {
			abilityCooldownText[i].text = ability[i].GetCooldownLeftRatio().ToString("F2");
			if (ability[i].GetCooldownLeftRatio() == 1) 
				abilityCooldownText[i].color = Color.green;
			else
				abilityCooldownText[i].color = Color.blue;
		}
	}

	public virtual void SwitchToInGameUI(bool toInGameUI) {
		inGameUI.SetActive(toInGameUI);
		choseAbilityUI.SetActive(!toInGameUI);
		if (!toInGameUI && choseAbilityUI.GetComponent<MenuManagementScript>()) {
			currentUI = choseAbilityUI.GetComponent<MenuManagementScript>();
			currentUI.OpenMenu();
		} else
			currentUI = null;

	}




}
