using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class MenuOption : MonoBehaviour
{

	public abstract void Select();

	void OnEnable() {
		GetComponent<Button>().onClick.AddListener(Select);
	}

	private void OnDisable() {
		GetComponent<Button>().onClick.RemoveListener(Select);
	}

}
