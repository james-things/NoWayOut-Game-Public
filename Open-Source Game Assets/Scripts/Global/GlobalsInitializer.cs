using System.Collections;
using System.Collections.Generic;
using NoWayOut.Scripts.Global;
using UnityEngine;

// Global variable initializer applied to initial scene event manager
public class GlobalsInitializer : MonoBehaviour
{
    // Initialize global vars
    void Start()
    {
        if (!GlobalVariables.Get<bool>("isGlobalInit"))
        {
            GlobalVariables.Set("isGlobalInit", true);
            GlobalVariables.Set("volume", 5);
            
            Debug.Log("applied global init");
        }
    }
}
