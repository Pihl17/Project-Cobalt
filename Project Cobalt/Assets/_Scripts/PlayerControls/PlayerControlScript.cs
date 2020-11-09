using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Abilities;


[RequireComponent(typeof(PlayerInput))]
public class PlayerControlScript : PlayerGUIDisplayerScript
{

	public RobotScript currentRobot;
	

	// Player input and output variables
	PlayerInput playerIn;
	bool movementFlag = false;
	Vector2 moveInput;
	Vector2 faceInput;
	float rotationInput;
	float rotateSpeed = 45f;


	Vector3 cameraOffset = new Vector3(0, 10, -5);
	float camFollowSpeed = 4.8f;
	float camTurnTime = 0.5f;
	float curCamTurnSpeed = 0.0f;
	Vector3 camNextPos;
	Vector3 camNextRot;


	// Start is called before the first frame update
	void Start()
    {
		playerIn = GetComponent<PlayerInput>();
		SwitchToInGameUI(false);
		playerIn.actions.FindAction("Move").performed += FlagRobotMovement;
		playerIn.actions.FindAction("Move").canceled += FlagRobotMovement;

		camNextPos = currentRobot.transform.position;

		PlayerStats.abilityInv.Add(new LaserGun());
		PlayerStats.abilityInv.Add(new RapidFireShot());
		PlayerStats.abilityInv.Add(new RechargableShield());
		PlayerStats.abilityInv.Add(new ChargableBlast());
		//PlayerStats.abilityInv.Add(new MortarStrike());

    }

    // Update is called once per frame
    void Update()
    {
		if (playerIn.actions.FindActionMap("Robot").enabled) {
			TurnRobot();

			currentRobot.UpdateAbilityCooldowns();
			UseAutomaticAbilities();

			UpdateAbilityCooldownDisplay(currentRobot.GetAbilities());
		} else if (playerIn.actions.FindActionMap("UI").enabled && currentUI) {
			SelectUIOption();
			NavigateUI();
		}

    }

	void FixedUpdate() {
		if (playerIn.actions.FindActionMap("Robot").enabled) {
			if (movementFlag)
				MoveRobot();
		}
	}

	void LateUpdate() {
		TurnCamera();
	}

	
	void MoveRobot() {
		moveInput = playerIn.actions.FindAction("Move").ReadValue<Vector2>();
		currentRobot.Move(currentRobot.transform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y)));
	}

	
	void FlagRobotMovement(InputAction.CallbackContext context) {
		movementFlag = context.phase == InputActionPhase.Performed;
	}


	void TurnRobot() {
		rotationInput = playerIn.actions.FindAction("Rotate").ReadValue<float>();
		currentRobot.transform.Rotate(Vector3.up * rotationInput * rotateSpeed * Time.deltaTime);
	}

	void TurnCamera() {
		camNextRot = new Vector3(Camera.main.transform.eulerAngles.x, currentRobot.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
		camNextRot.y = Mathf.SmoothDampAngle(Camera.main.transform.eulerAngles.y, camNextRot.y, ref curCamTurnSpeed, camTurnTime);
		Camera.main.transform.eulerAngles = camNextRot;

		camNextPos = Vector3.MoveTowards(camNextPos, currentRobot.transform.position, camFollowSpeed * Time.deltaTime);
		Camera.main.transform.position = camNextPos + Vector3.up * cameraOffset.y + Vector3.right * cameraOffset.z * Mathf.Sin(Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad) + Vector3.forward * cameraOffset.z * Mathf.Cos(Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad);
	}

	public void UseAbility0(InputAction.CallbackContext context) {
		currentRobot.UseAbility(0, context.phase);
	}

	public void UseAbility1(InputAction.CallbackContext context) {
		currentRobot.UseAbility(1, context.phase);
	}

	void UseAutomaticAbilities() {
		for (int i = 0; i < currentRobot.GetAbilityCount(); i++) {
			if (playerIn.actions.FindAction(string.Format("Ability{0}", i)).phase != InputActionPhase.Waiting)
				currentRobot.UseAbility(i, playerIn.actions.FindAction(string.Format("Ability{0}", i)).phase);
		}
	}

	public override void SwitchToInGameUI(bool toInGameUI) {
		base.SwitchToInGameUI(toInGameUI);
		if (toInGameUI) {
			playerIn.actions.FindActionMap("Robot").Enable();
			playerIn.actions.FindActionMap("UI").Disable();
		} else {
			playerIn.actions.FindActionMap("UI").Enable();
			playerIn.actions.FindActionMap("Robot").Disable();
		}

	}

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
