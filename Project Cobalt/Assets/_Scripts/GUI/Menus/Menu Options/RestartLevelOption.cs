using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevelOption : MenuOption
{
	public override void Select() {
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}
}
