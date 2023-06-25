using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An initializer for the scene initializer's initialize method :/
public class SceneInitializerInit : MonoBehaviour
{
    // Applied to event manager on first scene
    void Start()
    {
        SceneInitializer.Initialize();
    }
}
