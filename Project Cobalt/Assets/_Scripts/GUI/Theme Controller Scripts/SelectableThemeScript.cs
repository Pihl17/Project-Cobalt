using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class SelectableThemeScript : ThemeController
{

    protected override void ChangeTheme() {
        Selectable gui = GetComponent<Selectable>();
        GUIThemeConfig theme = GetTheme();
        if (gui && theme) {
            gui.colors = theme.ButtonColors;
        }
    }

}
