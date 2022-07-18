using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOption : MenuOption
{

	[SerializeField] string levelName = "";

	public override void Select() {
		SceneManager.LoadSceneAsync(levelName);
	}
}
