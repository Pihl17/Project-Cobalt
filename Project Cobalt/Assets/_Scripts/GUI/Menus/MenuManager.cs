using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeReference] protected GameObject[] menuCanvanses = new GameObject[0];

	void Start() {
		ChangeMenu(menuCanvanses[0]);
	}

	public void ChangeMenu(GameObject toMenu) {
		for (int i = 0; i < menuCanvanses.Length; i++) {
			if (menuCanvanses[i].Equals(toMenu)) {
				menuCanvanses[i].SetActive(true);

				Button[] buttons = toMenu.GetComponentsInChildren<Button>(true);
				for (int j = 0; j < buttons.Length; j++) {
					if (buttons[j].interactable) {
						EventSystem.current.SetSelectedGameObject(buttons[j].gameObject);
						break;
					}
				}

			} else
				menuCanvanses[i].SetActive(false);
		}

	}

}
