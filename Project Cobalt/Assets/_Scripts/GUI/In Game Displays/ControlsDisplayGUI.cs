using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Text;

public class ControlsDisplayGUI : MonoBehaviour
{

    public Text controlsText;
    PlayerInput playerIn;

    // Start is called before the first frame update
    void Start()
    {
        playerIn = FindObjectOfType<PlayerInput>();
        if (!playerIn)
            return;

        playerIn.onControlsChanged += UpdateDisplay;

        UpdateDisplay(playerIn);

    }

    void UpdateDisplay(PlayerInput player) {
        if (!controlsText)
            return;

        StringBuilder builder = new StringBuilder();

        builder.Append("Controls:").AppendLine();
        builder.Append("Gun        -> ").Append(player.actions.FindActionMap("Mech").FindAction("PrimaryFire").GetBindingDisplayString(InputBinding.DisplayStringOptions.DontUseShortDisplayNames)).AppendLine();
        builder.Append("Heavy      -> ").Append(player.actions.FindActionMap("Mech").FindAction("SecondaryFire").GetBindingDisplayString(InputBinding.DisplayStringOptions.DontUseShortDisplayNames)).AppendLine();
        builder.Append("Artillery  -> ").Append(player.actions.FindActionMap("Mech").FindAction("ArtilleryFire").GetBindingDisplayString(InputBinding.DisplayStringOptions.DontUseShortDisplayNames));

        controlsText.text = builder.ToString();

        if (GetComponent<RectTransform>())
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    void OnEnable() {
        if (playerIn)
            playerIn.onControlsChanged += UpdateDisplay;
    }

    void OnDisable() {
        if (playerIn)
            playerIn.onControlsChanged -= UpdateDisplay;
    }

}
