using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOption : MenuOption
{

	private void Awake() {
#if UNITY_WEBGL
		gameObject.SetActive(false);
#endif
	}

	public override void Select() {
		Application.Quit();
	}

}
