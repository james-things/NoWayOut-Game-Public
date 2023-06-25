using NoWayOut.Scripts.Game;
using UnityEngine;

// On/Off Object Controller 
// This is meant to control the state of screens
// which can be toggled on and off by user interaction.
// These screens subscribe the light switch turn off event -James
namespace NoWayOut.Scripts.Gameplay
{
    public class OnOffController : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject toggleableObject;
        [SerializeField] private bool isOn;
        [SerializeField] private Transform controllingSwitch;
        [SerializeField] private AudioClip activateSound;
        [SerializeField] private AudioClip deactivateSound;
        [SerializeField] private GameObject player;
        [SerializeField] private bool reportsPower;
        [SerializeField] private GameObject powerController;

        private TimeLimit _timeLimit;
        private LightSwitchController _controllingSwitch;
        private GameObject _controlledObject;
        private PowerController _powerController;

        // Grab controller and intialize state
        void Start()
        {
            _powerController = powerController.GetComponent<PowerController>();

            _controllingSwitch = controllingSwitch.GetComponent<LightSwitchController>();
            _controlledObject = toggleableObject;

            if (isOn)
            {
                _controlledObject.SetActive(true);
            }
            else
            {
                _controlledObject.SetActive(false);
            }

            // for logging
            _timeLimit = player.GetComponent<TimeLimit>();
        }

        void Update()
        {
            if (!_controllingSwitch.GetIsOn())
            {
                ForceOff();
            }
        }

        public void TurnOn()
        {
            if (!isOn) ToggleOn();
        }
        
        public void ForceOff()
        {
            if (isOn) ToggleOn();
        }

        public bool GetIsOn()
        {
            return isOn;
        }

        public bool Interact(Interactor interactor)
        {
            var switchable = _powerController.GetPowerIsOn() && _controllingSwitch.GetIsOn();
            // Redundant parenthesis added for clarity
            string stateTransition = switchable ? (isOn ? "[on -> off]" : "[off -> on]") : ("[power off]");

            Debug.Log("Interaction: " + name + " " + stateTransition + " at " + _timeLimit.timeLeftString + ".");

            if (switchable) ToggleOn();
            else
            {
                if (deactivateSound)
                    AudioUtility.CreateSFX(deactivateSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
            }

            return true;
        }

        private void ToggleOn()
        {
            if (isOn)
            {
                _controlledObject.SetActive(false);
                isOn = false;
                if (reportsPower && powerController != null)
                    _powerController.UpdateElectronicDevice(gameObject.name, false);

                if (deactivateSound)
                    AudioUtility.CreateSFX(deactivateSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
            }
            else
            {
                _controlledObject.SetActive(true);
                isOn = true;
                if (reportsPower && powerController != null)
                    _powerController.UpdateElectronicDevice(gameObject.name, true);

                if (activateSound)
                    AudioUtility.CreateSFX(activateSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
            }
        }
    }
}
