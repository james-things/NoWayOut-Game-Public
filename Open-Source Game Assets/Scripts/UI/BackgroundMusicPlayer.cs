using UnityEngine;
using UnityEngine.SceneManagement;

// Class to play alternate background track outside of main scene,
// ended up not using due to unresolved problems
namespace NoWayOut.Scripts.UI
{
    public class BackgroundMusicPlayer : MonoBehaviour
    {
        private bool _initialized = false;
        private AudioSource _audioSource;

        private void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
            _audioSource = GetComponent<AudioSource>();
        }
        
        void Start() {
            if (_initialized == false) {
                _initialized = true;
                // adds this to the 'activeSceneChanged' callbacks if not already initialized.
                SceneManager.activeSceneChanged += OnSceneWasLoaded;
            }
            _audioSource = GetComponent<AudioSource>();
            Play();
        }

        public void Play()
        {
            if (_audioSource.isPlaying) return;
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }
        
        private void OnSceneWasLoaded (Scene from, Scene to)
        {
            if (to.name.Equals("MainScene")) Stop();
            else Play();
        }
    }
}

