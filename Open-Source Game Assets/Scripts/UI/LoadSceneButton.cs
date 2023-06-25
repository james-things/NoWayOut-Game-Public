using System.Collections;
using NoWayOut.Scripts.Global;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NoWayOut.Scripts.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        public string SceneName = "";

        public void QuitGame()
        {
            Application.Quit();
        }

        public void LoadTargetScene()
        {
            StartCoroutine(SceneChangeWaiter());
        }

        public void LoadEasyMode()
        {
            StartCoroutine(EasyModeWaiter());
        }

        public void LoadHardMode()
        {
            StartCoroutine(HardModeWaiter());
        }

        IEnumerator EasyModeWaiter()
        {
            GlobalVariables.Set("difficulty", "easy");
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(SceneName);
        }

        IEnumerator HardModeWaiter()
        {
            GlobalVariables.Set("difficulty", "hard");
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(SceneName);
        }
        
        IEnumerator SceneChangeWaiter()
        {
            //Wait for 1 second
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(SceneName);
        }
    }
}
