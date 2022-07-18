using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

	void RestartLevel() {
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}

	void OnEnable() {
		PlayerControl.OnPlayerDestroy += RestartLevel;
	}

	void OnDisable() {
		PlayerControl.OnPlayerDestroy -= RestartLevel;
	}

}
