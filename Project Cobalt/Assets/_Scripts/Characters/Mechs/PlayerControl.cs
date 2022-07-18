using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerControl : MonoBehaviour
{

	CombatMech playerMech;
	PlayerInput playerIn;

	public delegate void PlayerInitialisedEvent(CombatMech mechScript);
	public static event PlayerInitialisedEvent OnPlayerInitialisation;
	public delegate void PlayerHealthChangeEvent(float health);
	public static event PlayerHealthChangeEvent OnPlayerHealthChange;
	public delegate void PlayerDestroyedEvent();
	public static event PlayerDestroyedEvent OnPlayerDestroy;

	void Start() {
		Initialisation();
	}

	void Initialisation() {
		playerIn = GetComponent<PlayerInput>();
		playerMech = GetComponent<CombatMech>();
		AddListenToMechEvents();
		AddListenToPlayerInputs();

		OnPlayerInitialisation?.Invoke(playerMech);
	}

	void AddListenToPlayerInputs() {
		playerIn.actions.FindAction("PrimaryFire").performed += FirePrimaryWeapon;
		playerIn.actions.FindAction("PrimaryFire").canceled += FirePrimaryWeapon;
		playerIn.actions.FindAction("SecondaryFire").performed += FireSecondaryWeapon;
		playerIn.actions.FindAction("SecondaryFire").canceled += FireSecondaryWeapon;
		playerIn.actions.FindAction("ArtilleryFire").performed += FireArtilleryWeapon;
		playerIn.actions.FindAction("ArtilleryFire").canceled += FireArtilleryWeapon;
	}

	void FixedUpdate() {
		if (playerIn.actions.FindActionMap("Mech").enabled)
			MovementInputs();
	}

	void MovementInputs() {
		MoveInput();
		TurnInput();
	}

	void MoveInput() {
		playerMech.Walk(playerIn.actions.FindAction("Move").ReadValue<Vector2>());
	}

	void TurnInput() {
		playerMech.Turn(playerIn.actions.FindAction("Turn").ReadValue<float>());
	}

	void AnnounchHealthChange(float remainingHealth, float healthChange) {
		OnPlayerHealthChange?.Invoke(remainingHealth);
	}

	void AnnounchPlayerDestroy() {
		OnPlayerDestroy?.Invoke();
	}

	void FirePrimaryWeapon(InputAction.CallbackContext context) {
		StartFire(0, context);
	}

	void FireSecondaryWeapon(InputAction.CallbackContext context) {
		StartFire(1, context);
	}

	void FireArtilleryWeapon(InputAction.CallbackContext context) {
		StartFire(2, context);
	}

	void StartFire(int index, InputAction.CallbackContext context) {
		if (!PauseMenu.paused || context.action.phase == InputActionPhase.Canceled)
			playerMech.SetAutomaticFire(index, context.action.phase == InputActionPhase.Performed);
	}

	void AddListenToMechEvents() {
		playerMech.OnHealthChanged += AnnounchHealthChange;
		playerMech.OnDestroy += AnnounchPlayerDestroy;
	}


	private void OnEnable() {
		if (playerMech) {
			AddListenToMechEvents();
			AddListenToPlayerInputs();
		}
	}

	private void OnDisable() {
		playerMech.OnHealthChanged -= AnnounchHealthChange;
		playerMech.OnDestroy -= AnnounchPlayerDestroy;
		playerIn.actions.FindAction("PrimaryFire").performed -= FirePrimaryWeapon;
		playerIn.actions.FindAction("PrimaryFire").canceled -= FirePrimaryWeapon;
		playerIn.actions.FindAction("SecondaryFire").performed -= FireSecondaryWeapon;
		playerIn.actions.FindAction("SecondaryFire").canceled -= FireSecondaryWeapon;
		playerIn.actions.FindAction("ArtilleryFire").performed -= FireArtilleryWeapon;
		playerIn.actions.FindAction("ArtilleryFire").canceled -= FireArtilleryWeapon;
	}

}
