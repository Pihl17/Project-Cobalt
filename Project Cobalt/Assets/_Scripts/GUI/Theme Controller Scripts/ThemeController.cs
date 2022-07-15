using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThemeController : MonoBehaviour
{

    void Awake() {
        ChangeTheme();
    }

    protected abstract void ChangeTheme();

    protected GUIThemeConfig GetTheme() {
        return Resources.Load<GUIThemeConfig>("Default GUI Theme Config");
    }

}
