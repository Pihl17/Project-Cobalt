using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerHealthUpdateScript : MonoBehaviour
{

	Text text;
	
	// Start is called before the first frame update
    void Awake()
    {
		text = GetComponent<Text>();
    }

	void UpdateHealthText(float healthRemaining, float healthLost) {
		if (text)
			text.text = healthRemaining.ToString();
	}

	private void OnEnable() {
		if (GameObject.Find("PlayerMech") && GameObject.Find("PlayerMech").GetComponent<PlayerControlledMech>()) {
			GameObject.Find("PlayerMech").GetComponent<PlayerControlledMech>().OnDamaged += UpdateHealthText;
			UpdateHealthText(GameObject.Find("PlayerMech").GetComponent<PlayerControlledMech>().mechConfig.MaxHealth, 0f);
		}
	}

	private void OnDisable() {
		GameObject playerMech = GameObject.Find("PlayerMech");
		if (playerMech && playerMech.GetComponent<PlayerControlledMech>())
			playerMech.GetComponent<PlayerControlledMech>().OnDamaged -= UpdateHealthText;
	}

}
