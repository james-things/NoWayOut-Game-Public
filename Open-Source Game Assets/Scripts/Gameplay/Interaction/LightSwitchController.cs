using NoWayOut.Scripts.Game;
using UnityEngine;

// Controller script for Light Switch
// Simple on/off state toggle - "electronic devices"
// subscribe to the state of the switch and respond
// conditionally on Update() -James
namespace NoWayOut.Scripts.Gameplay
{
    public class LightSwitchController : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool isOn;
        [SerializeField] private AudioClip switchOnSound;
        [SerializeField] private AudioClip switchOffSound;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject powerController;

        private PowerController _powerController;
        private TimeLimit _timeLimit;

        private void Start()
        {
            // for logging
            _timeLimit = player.GetComponent<TimeLimit>();

            _powerController = powerController.GetComponent<PowerController>();
        }

        // Light switch interaction
        public bool Interact(Interactor interactor)
        {
            bool masterPower = _powerController.GetPowerIsOn();

            // Redundant parenthesis added for clarity
            string stateTransition = masterPower ? (isOn ? "[on -> off]" : "[off -> on]") : ("[power off]");
            Debug.Log("Interaction: " + name + " " + stateTransition + " at " + _timeLimit.timeLeftString + ".");

            if (masterPower)
            {
                if (isOn)
                {
                    isOn = false;
                    if (switchOffSound)
                        AudioUtility.CreateSFX(switchOffSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);

                }
                else
                {
                    isOn = true;
                    if (switchOnSound)
                        AudioUtility.CreateSFX(switchOnSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
                }
            }
            else
            {
                if (switchOffSound)
                    AudioUtility.CreateSFX(switchOffSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
            }

            return true;
        }

        public bool GetIsOn()
        {
            return isOn;
        }

        public void ForceOff()
        {
            if (isOn) isOn = false;
        }
    }
}
