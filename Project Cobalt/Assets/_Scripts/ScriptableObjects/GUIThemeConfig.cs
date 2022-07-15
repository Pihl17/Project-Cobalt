using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GUI Theme Config", menuName = "ScriptableObject/GUI Theme Config", order = 0)]
public class GUIThemeConfig : ScriptableObject
{

    [SerializeField] ColorBlock buttonColors = new ColorBlock();
    public ColorBlock ButtonColors { get { return buttonColors; } }



}
