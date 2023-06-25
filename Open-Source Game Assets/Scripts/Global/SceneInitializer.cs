using NoWayOut.Scripts.Game;
using NoWayOut.Scripts.Gameplay;
using NoWayOut.Scripts.Global;
using UnityEngine;

// A static (global) class to initialize the scene on load
public static class SceneInitializer
{
    private static bool initialized = false;
    private static bool difficultyApplied = false;
    private static DifficultyManager _difficultyManager;

    // Initialize the initializer (not the scene!)
    public static void Initialize () {
        if (initialized == false) {
            initialized = true;
            // adds this to the 'activeSceneChanged' callbacks if not already initialized.
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnSceneWasLoaded;
        }
    }

    private static void OnSceneWasLoaded (UnityEngine.SceneManagement.Scene from, UnityEngine.SceneManagement.Scene to) {
        Debug.Log("Successfully hooked scene change to: " + to.name);

        if (to.name == "MainScene")
        {
            // Hopefully correctly maintain the user's volume setting from global state
            AudioUtility.SetMasterVolume((float)GlobalVariables.Get<int>("volume") / 10);
            
            Debug.Log("The difficulty to load is: " + GlobalVariables.Get<string>("difficulty"));
            if (!difficultyApplied)
            {
                _difficultyManager = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>();
                
                if (GlobalVariables.Get<string>("difficulty").Equals("hard"))
                {
                    _difficultyManager.ApplyHardMode();
                    difficultyApplied = true;
                }
                else
                {
                    _difficultyManager.ApplyEasyMode();
                    difficultyApplied = true;
                }
            }
        }
        if (!to.name.Equals("MainScene"))
        {
            Debug.Log("Toggling difficulty applied to false");
            // if switching away from MainScene, reset scene initialize state
            difficultyApplied = false;
        }
    }
}
