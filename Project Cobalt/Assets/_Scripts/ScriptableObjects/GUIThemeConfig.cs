using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GUI Theme Config", menuName = "ScriptableObject/GUI Theme Config", order = 0)]
public class GUIThemeConfig : ScriptableObject
{
    [Header("Text Colors")]
    [SerializeField] Color titleColor = new Color(1, 1, 1);
    public Color TitleColor { get { return titleColor; } }
    [SerializeField] Color headerColor = new Color(1, 1, 1);
    public Color HeaderColor { get { return headerColor; } }
    [SerializeField] Color textColor = new Color(1, 1, 1);
    public Color TextColor { get { return textColor; } }

    [Header("Menu Button Colors")]
    [SerializeField] ColorBlock buttonColors = new ColorBlock();
    public ColorBlock ButtonColors { get { return buttonColors; } }
    [Space]
    [SerializeField] Color buttonTextColor = new Color(1, 1, 1);
    public Color ButtonTextColor { get { return buttonTextColor; } }

    [Header("Display Colors")]
    [SerializeField] Color healthColor = new Color(1, 1, 1);
    public Color HealthColor { get { return healthColor; } }
    [SerializeField] Color ammoColor = new Color(1, 1, 1);
    public Color AmmoColor { get { return ammoColor; } }
    [SerializeField] Color ammoUpgradeColor = new Color(1, 1, 1);
    public Color AmmoUpgradeColor { get { return ammoUpgradeColor; } }

}
