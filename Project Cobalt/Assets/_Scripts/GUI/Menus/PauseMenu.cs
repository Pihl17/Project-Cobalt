using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MenuManager
{

	public static bool paused = false;

	public GameObject pausePanal;

	public PlayerInput playerInput;

	void Start() {
		Pause(false);
	}

	public void Pause(bool pause) {
		paused = pause;
		pausePanal.SetActive(pause);
		if (pause)
			ChangeMenu(menuCanvanses[0]);
	}

	public void PauseGame(InputAction.CallbackContext context) {
		Pause(!paused);
	}

	void OnEnable() {
		playerInput.actions.FindAction("Pause").performed += PauseGame;
	}

	void OnDisable() {
		playerInput.actions.FindAction("Pause").performed -= PauseGame;
	}

}
