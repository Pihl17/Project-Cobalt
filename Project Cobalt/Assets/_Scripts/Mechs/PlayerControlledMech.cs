using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerControlledMech : CombatMech
{

	PlayerInput playerIn;
	Vector2 moveInput;

	public delegate void PlayerInitialisedEvent(PlayerControlledMech playerScript);
	public static event PlayerInitialisedEvent OnPlayerInitialisation;
	public delegate void PlayerHealthChangeEvent(float health);
	public static event PlayerHealthChangeEvent OnPlayerDamaged;
	public static event PlayerHealthChangeEvent OnPlayerHealed;
	public delegate void PlayerDestroyedEvent();
	public static event PlayerDestroyedEvent OnPlayerDestroy;

	void Start() {
		Initialisation();
	}

	protected override void Initialisation() {
		base.Initialisation();
		playerIn = GetComponent<PlayerInput>();

		playerIn.actions.FindAction("PrimaryFire").performed += FirePrimaryWeapon;
		playerIn.actions.FindAction("PrimaryFire").canceled += FirePrimaryWeapon;
		playerIn.actions.FindAction("SecondaryFire").performed += FireSecondaryWeapon;
		playerIn.actions.FindAction("SecondaryFire").canceled += FireSecondaryWeapon;
		playerIn.actions.FindAction("ArtilleryFire").performed += FireArtilleryWeapon;
		playerIn.actions.FindAction("ArtilleryFire").canceled += FireArtilleryWeapon;

		OnPlayerInitialisation?.Invoke(this);
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
		Move(playerIn.actions.FindAction("Move").ReadValue<Vector2>());
	}

	void TurnInput() {
		Turn(playerIn.actions.FindAction("Turn").ReadValue<float>());
	}

	public override void Damage(float amount) {
		base.Damage(amount);
		OnPlayerDamaged?.Invoke(Health);
	}

	public override void Heal(float amount) {
		base.Heal(amount);
		OnPlayerHealed?.Invoke(Health);
	}

	protected override void Destroy() {
		base.Destroy();
		if (OnPlayerDestroy != null)
			OnPlayerDestroy.Invoke();
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
		FireWeapon(index, context.phase);
		if (context.action.phase == InputActionPhase.Performed)
			StartCoroutine(AutomaticFire(index, weapons[index].Cooldown));
		else if (context.action.phase == InputActionPhase.Canceled)
			weapons[index].triggerHeldDown = false;
	}

	IEnumerator AutomaticFire(int index, float waitTime) {
		if (weapons[index] != null) {
			weapons[index].triggerHeldDown = true;
			while (weapons[index].triggerHeldDown) {
				FireWeapon(index, InputActionPhase.Started);
				yield return new WaitForSeconds(waitTime);
			}
		}
	}

}
