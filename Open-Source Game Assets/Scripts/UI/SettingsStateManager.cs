using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using NoWayOut.Scripts.Global;
using TMPro;
using UnityEngine;

public class SettingsStateManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI volumeText;

    private TextMeshProUGUI _volumeText;
    // Start is called before the first frame update
    void Start()
    {
        _volumeText = volumeText;
        volumeText.text = (GlobalVariables.Get<float>("volume") * 10).ToString();
    }

    private void Update()
    {
        volumeText.text = (GlobalVariables.Get<float>("volume") * 10).ToString();
    }
}
