using NoWayOut.Scripts.Game;
using TMPro;
using UnityEngine;

namespace NoWayOut.Scripts.Gameplay
{
// note: power subscribes to lightcontroller
    public class PinDisplayController : MonoBehaviour
    {
        // [SerializeField] private string tweenName;
        public TextMeshPro displayText;
        // [SerializeField] private int pinCode;
        [SerializeField] private LightController lightController;
        [SerializeField] private AudioClip pressSound;
        [SerializeField] private AudioClip successSound;
        [SerializeField] private AudioClip failureSound;
        [SerializeField] private GameObject controlledDoor;
        [SerializeField] private GameObject _player;

        private TimeLimit _timeLimit;
        private InteractableLockedDoor _controlledDoor;
        private MeshRenderer _meshRenderer;
        private int _pinCode;
        
        private bool _isOn = false;

        // Run before first frame
        void Start()
        {
            // Get main door controller
            _controlledDoor = controlledDoor.GetComponent<InteractableLockedDoor>();

            // Get isOn from _lightController
            _isOn = lightController.GetIsOn();

            // get mesh renderer within same gameobject as script
            _meshRenderer = GetComponentInParent<MeshRenderer>();

            // for logging
            _timeLimit = _player.GetComponent<TimeLimit>();
        }

        private void Update()
        {
            // Get isOn from _lightController for power mechanic
            _isOn = lightController.GetIsOn();

            // if power is out, force "off" state
            if (!_isOn)
            {
                displayText.text = "";
                _meshRenderer.enabled = false;
            }

            // if power is on and screen is "off" turn it on
            if (_isOn && !_meshRenderer.enabled)
            {
                _meshRenderer.enabled = true;
                displayText.text = "***";
            }
        }

        // Clear the display state
        public void ClearDisplay()
        {
            displayText.text = "";
        }

        // Reset the display state
        public void ResetDisplay()
        {
            Debug.Log("Interaction: " + name + " " + "[reset display]" + " at " + _timeLimit.timeLeftString + ".");

            displayText.text = "***";
            // button tap sound
            if (pressSound) AudioUtility.CreateSFX(pressSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        }

        public void SetPinCode(int newCode)
        {
            _pinCode = newCode;
        }
        
        // Update the state, clearing it if the value is exactly "0"
        public void UpdateDisplayText(string newText)
        {
            Debug.Log("Interaction: " + name + " " + "[button press: " + newText + "]" + " at " +
                      _timeLimit.timeLeftString + ".");

            if (displayText.text is "***" or "Error!" or "Success!")
                ClearDisplay();
            displayText.text += newText;

            // button tap sound
            if (pressSound) AudioUtility.CreateSFX(pressSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        }

        // Process equation via NCalc
        public void ProcessSubmit()
        {
            Debug.Log("Interaction: " + name + " " + "[submit " + displayText.text + " as pin]" + " at " +
                      _timeLimit.timeLeftString + ".");

            // button tap sound
            if (pressSound) AudioUtility.CreateSFX(pressSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);

            int submission = int.Parse(displayText.text);
            if (submission == _pinCode)
            {
                Debug.Log("Interaction: " + name + " " + "[pin accepted]" + " at " + _timeLimit.timeLeftString + ".");

                displayText.text = "Success!";
                // play good sound
                // DOTween.Play(tweenName);
                if (successSound)
                    AudioUtility.CreateSFX(successSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);

                _controlledDoor.UnlockAndOpen();
            }
            else
            {
                Debug.Log("Interaction: " + name + " " + "[pin rejected]" + " at " + _timeLimit.timeLeftString + ".");

                displayText.text = "Error!";
                if (failureSound)
                    AudioUtility.CreateSFX(failureSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
            }
        }
    }
}
