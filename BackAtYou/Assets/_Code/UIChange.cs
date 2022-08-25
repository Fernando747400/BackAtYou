using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIChange : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI _ForceText;
    [SerializeField] private TextMeshProUGUI _AxeText;
    [SerializeField] private TextMeshProUGUI _TeleportText;
    [SerializeField] private AxeThrow axeStatus;

    private void Update()
    {
        if (axeStatus.canShoot)
        {
            _AxeText.text = "Ready";
        }
        else
        {
            _AxeText.text = "Reloading";
        }

        _ForceText.text = axeStatus.ThrowForce.ToString();

        if (axeStatus.canTeleport)
        {
            _TeleportText.text = "Ready";
        }
        else
        {
            _TeleportText.text = "On cooldown";
        }
    }
}
