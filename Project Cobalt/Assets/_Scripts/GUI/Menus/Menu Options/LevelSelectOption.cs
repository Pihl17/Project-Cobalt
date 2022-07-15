using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectOption : ChangeMenuOption
{

	[SerializeField] string levelName = "";

	public override void Select() {
		base.Select();
		LevelSelection.levelName = levelName;
	}

}
