using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextThemeScript : ThemeController
{

	enum TextType { title, header, regular, health, ammo, upgradeAmmo }
	[SerializeField] TextType textType = TextType.regular;
	
	protected override void ChangeTheme() {
		Text gui = GetComponent<Text>();
		GUIThemeConfig theme = GetTheme();
		if (gui && theme) {
			switch (textType) {
				case TextType.title:
					gui.color = theme.TitleColor;
					break;
				case TextType.header:
					gui.color = theme.HeaderColor;
					break;
				case TextType.regular:
					gui.color = theme.TextColor;
					break;
				case TextType.health:
					gui.color = theme.HealthColor;
					break;
				case TextType.ammo:
					gui.color = theme.AmmoColor;
					break;
				case TextType.upgradeAmmo:
					gui.color = theme.AmmoUpgradeColor;
					break;
			}
		}
	}
}
