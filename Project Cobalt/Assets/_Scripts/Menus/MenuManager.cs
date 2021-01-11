using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{

	[SerializeField] PlayerInput playerIn;

	public MenuOption option;

	void Awake() {
		if (!playerIn)
			playerIn = GetComponent<PlayerInput>();
	}

	void SelectOption(InputAction.CallbackContext context) {
		option.Select();
	}

	void NavigateMenu(InputAction.CallbackContext context) {
		Vector2 input = context.action.ReadValue<Vector2>();
		if (input.y > 0)
			option = option.GetNearbour(MenuOption.Direction.Up);
		else if (input.y < 0)
			option = option.GetNearbour(MenuOption.Direction.Down);
	}


	void OnEnable() {
		playerIn.actions.FindAction("Select").performed += SelectOption;
		playerIn.actions.FindAction("Navigate").performed += NavigateMenu;
	}

	void OnDisable() {
		playerIn.actions.FindAction("Select").performed -= SelectOption;
		playerIn.actions.FindAction("Navigate").performed -= NavigateMenu;
	}

}
