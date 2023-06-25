using System;
using System.Collections;
using System.Collections.Generic;
using NoWayOut.Scripts.Game;
using NoWayOut.Scripts.Global;
using TMPro;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI volumeText;

    private TextMeshProUGUI _volumeText;
    private int _curVol;

    private void Start()
    {
        _volumeText = volumeText;
        _curVol = GlobalVariables.Get<int>("volume");
        _volumeText.text = _curVol.ToString();
    }

    public void IncreaseVolume()
    {
        int curVolumeSetting = int.Parse(_volumeText.text);
        if (curVolumeSetting != 10)
        {
            int newVolume = curVolumeSetting + 1;

            _volumeText.text = newVolume.ToString();
            GlobalVariables.Set("volume", newVolume);
            AudioUtility.SetMasterVolume((float)newVolume / 10);
            Debug.Log("Set volume to: " + newVolume);
        }
    }
    
    public void DecreaseVolume()
    {
        int curVolumeSetting = int.Parse(_volumeText.text);
        if (curVolumeSetting != 0)
        {
            int newVolume = curVolumeSetting - 1;

            _volumeText.text = newVolume.ToString();
            GlobalVariables.Set("volume", newVolume);
            AudioUtility.SetMasterVolume((float)newVolume / 10);
            Debug.Log("Set volume to: " + newVolume);
        }
    }
}
