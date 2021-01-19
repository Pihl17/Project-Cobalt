using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayOption : MenuOption
{

	[SerializeField] string level = "";

	public override void Select() {
		SceneManager.LoadSceneAsync(level);
	}

}
