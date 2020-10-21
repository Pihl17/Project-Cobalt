using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Abilities;


[RequireComponent(typeof(PlayerInput))]
public class PlayerControlScript : PlayerGUIDisplayerScript
{

	public RobotScript currentRobot;
	public List<RobotScript> playerRobots = new List<RobotScript>();
	

	// Player input and output variables
	PlayerInput playerIn;
	Vector2 moveInput;
	Vector2 faceInput;

	public enum ControlStyle {TopDown, ThirdPerson};
	public ControlStyle controlStyle;
	Vector3 cameraOffset = new Vector3(0, 16.22f, -3.4f);



    // Start is called before the first frame update
    void Start()
    {
		playerIn = GetComponent<PlayerInput>();
		SwitchToInGameUI(false);

		PlayerStats.abilityInv.Add(new LaserGun());
		//PlayerStats.abilityInv.Add(new RapidFireShot());
		PlayerStats.abilityInv.Add(new RechargableShield());
		PlayerStats.abilityInv.Add(new ChargableBlast());
		PlayerStats.abilityInv.Add(new MortarStrike());

		if (controlStyle == ControlStyle.ThirdPerson) {
			cameraOffset = new Vector3(0, 10, -6);
			Camera.main.transform.localEulerAngles = new Vector3(50, 0, 0);
		}

    }

    // Update is called once per frame
    void Update()
    {
		if (playerIn.actions.FindActionMap("Robot").enabled) {
			MoveRobot();
			TurnRobot();

			currentRobot.UpdateAbilityCooldowns();
			UseAutomaticAbilities();

			UpdateAbilityCooldownDisplay(currentRobot.GetAbilities());
		} else if (playerIn.actions.FindActionMap("UI").enabled && currentUI) {
			SelectUIOption();
			NavigateUI();
		}

    }

	void LateUpdate() {
		TurnCamera();
	}

	void MoveRobot() {
		moveInput = playerIn.actions.FindAction("Move").ReadValue<Vector2>();
		if (controlStyle == ControlStyle.TopDown) {
			currentRobot.Move(new Vector3(moveInput.x, 0, moveInput.y));
		} else if (controlStyle == ControlStyle.ThirdPerson) {
			currentRobot.Move(currentRobot.transform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y)));
		}
	}

	void TurnRobot() {
		if (controlStyle == ControlStyle.TopDown) {
			faceInput = playerIn.actions.FindAction("Look").ReadValue<Vector2>();
			currentRobot.Turn(new Vector3(faceInput.x - Camera.main.pixelWidth/2, 0, faceInput.y - Camera.main.pixelHeight/2).normalized);
		} else if (controlStyle == ControlStyle.ThirdPerson) {
			faceInput = playerIn.actions.FindAction("LookDelta").ReadValue<Vector2>();
			currentRobot.transform.Rotate(Vector3.up * faceInput.x*0.2f);
		}
	}

	void TurnCamera() {
		Camera.main.transform.position = currentRobot.transform.position + Vector3.up * cameraOffset.y + Vector3.right * cameraOffset.z * Mathf.Sin(currentRobot.transform.eulerAngles.y * Mathf.Deg2Rad) + Vector3.forward * cameraOffset.z * Mathf.Cos(currentRobot.transform.eulerAngles.y * Mathf.Deg2Rad);
		Camera.main.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, currentRobot.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
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

	public void SwitchRobot(InputAction.CallbackContext context) {
		if (context.phase == InputActionPhase.Performed) {
			RobotScript previousRobot = currentRobot;
			currentRobot = playerRobots[0];
			playerRobots[0] = previousRobot;
		}
	}

	public void CreateNewRobot(InputAction.CallbackContext context) {
		if (context.phase == InputActionPhase.Performed) {
			// TODO: Set up the menu for choosing abilities, and make it so that clicking the confirm on that creates a new prefab and add it to this script.
		}
	}

	public void AddRobot(RobotScript robot) {
		// TODO: Add the robot to the currentRobot variable, or to the playerRobots list if there is already a current robot
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
