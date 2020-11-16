using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMechControllerScript : MechWeaponSystemScript {

	// Player input and output variables
	PlayerInput playerIn;
	//bool movementFlag = false;
	Vector2 moveInput;
	Vector2 faceInput;


	public delegate void PlayerDestroyedEvent();
	public static event PlayerDestroyedEvent OnPlayerDestroyed;
	

	protected override void Initialisation() {
		base.Initialisation();
		playerIn = GetComponent<PlayerInput>();

		playerIn.actions.FindAction("Ability0").performed += UseAbility0;
		playerIn.actions.FindAction("Ability0").canceled += UseAbility0;
		playerIn.actions.FindAction("Ability1").performed += UseAbility1;
		playerIn.actions.FindAction("Ability1").canceled += UseAbility1;

		PlayerStats.abilityInv.Add(new Abilities.LaserGun());
		PlayerStats.abilityInv.Add(new Abilities.RapidFireShot());
		PlayerStats.abilityInv.Add(new Abilities.RechargableShield());
		PlayerStats.abilityInv.Add(new Abilities.ChargableBlast());
	}

	void Update() {
		FindLockOnTarget();
		if (playerIn.actions.FindActionMap("Robot").enabled) {
			TurnInput();

			UpdateAbilityCooldowns();
			UseAutomaticAbilities();
		}
	}

	void FixedUpdate() {
		if (playerIn.actions.FindActionMap("Robot").enabled) {
			MoveInput();
		}
	}

	void MoveInput() {
		moveInput = playerIn.actions.FindAction("Move").ReadValue<Vector2>();
		Move(transform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y)));
	}

	void TurnInput() {
		Turn(playerIn.actions.FindAction("Rotate").ReadValue<float>());
	}


	public void UseAbility0(InputAction.CallbackContext context) {
		UseAbility(0, context.phase);
	}

	public void UseAbility1(InputAction.CallbackContext context) {
		UseAbility(1, context.phase);
	}

	void UseAutomaticAbilities() {
		for (int i = 0; i < GetAbilityCount(); i++) {
			if (playerIn.actions.FindAction(string.Format("Ability{0}", i)).phase != InputActionPhase.Waiting)
				UseAbility(i, playerIn.actions.FindAction(string.Format("Ability{0}", i)).phase);
		}
	}

	protected override void Destroy() {
		if (OnPlayerDestroyed != null)
			OnPlayerDestroyed.Invoke();
		this.enabled = false;
	}


	// UI Navigation
	protected MenuManagementScript currentUI;



	void NavigateUI() {
		if (navigateUiFlag) {
			currentUI.Navigate(playerIn.actions.FindAction("Navigate").ReadValue<Vector2>());
			navigateUiFlag = false;
		}
	}

	bool navigateUiFlag = false;
	public void NavigateUI(InputAction.CallbackContext context) {
		if (context.phase == InputActionPhase.Performed) {
			navigateUiFlag = true;
		}
	}

	void SelectUIOption() {
		if (selectUiFlag) {
			currentUI.SelectCurrentOption();
			selectUiFlag = false;
		}
	}

	bool selectUiFlag = false;
	public void SelectUIOption(InputAction.CallbackContext context) {
		if (context.phase == InputActionPhase.Performed) {
			selectUiFlag = true;
		}
	}

}
