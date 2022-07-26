using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Text;

public class ControlsDisplayGUI : MonoBehaviour
{

    public Text controlsText;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerInput playerIn = FindObjectOfType<PlayerInput>();
        if (!playerIn || !controlsText)
            return;
        
        StringBuilder builder = new StringBuilder();

        builder.Append("Controls:").AppendLine();
        builder.Append("Gun        -> ").Append(playerIn.actions.FindActionMap("Mech").FindAction("PrimaryFire").GetBindingDisplayString(InputBinding.DisplayStringOptions.DontUseShortDisplayNames)).AppendLine();
        builder.Append("Heavy      -> ").Append(playerIn.actions.FindActionMap("Mech").FindAction("SecondaryFire").GetBindingDisplayString(InputBinding.DisplayStringOptions.DontUseShortDisplayNames)).AppendLine();
        builder.Append("Artillery  -> ").Append(playerIn.actions.FindActionMap("Mech").FindAction("ArtilleryFire").GetBindingDisplayString(InputBinding.DisplayStringOptions.DontUseShortDisplayNames));

        controlsText.text = builder.ToString();
    }

    


}
