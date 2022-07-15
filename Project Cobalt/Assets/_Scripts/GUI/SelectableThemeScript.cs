using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class SelectableThemeScript : MonoBehaviour
{

    void Awake() {
        ChangeTheme();
    }

    void ChangeTheme() {
        Selectable gui = GetComponent<Selectable>();
        GUIThemeConfig theme = Resources.Load<GUIThemeConfig>("Default GUI Theme Config");
        if (gui && theme) {
            gui.colors = theme.ButtonColors;
        }
    }

}
