using System.Collections.Generic;
using NoWayOut.Scripts.Game;
using UnityEngine;

// Master power controller for power puzzle
// Will be applied to breaker object to be added in bathroom
namespace NoWayOut.Scripts.Gameplay
{
    public class PowerController : MonoBehaviour
    {
        // this feels terribly inefficient...
        [SerializeField] private bool isOn;
        [SerializeField] private GameObject livSwitchContainer;
        [SerializeField] private GameObject foySwitchContainer;
        [SerializeField] private GameObject batSwitchContainer;
        [SerializeField] private GameObject kitSwitchContainer;
        [SerializeField] private GameObject bedSwitchContainer;
        [SerializeField] private GameObject tvContainer;
        [SerializeField] private GameObject bedroomCrtContainer;
        [SerializeField] private GameObject kitchenCrtContainer;
        [SerializeField] private AudioClip activateSound;
        [SerializeField] private AudioClip deactivateSound;
        [SerializeField] private GameObject breakerContainer;

        private LightSwitchController _livSwitch;
        private LightSwitchController _foySwitch;
        private LightSwitchController _batSwitch;
        private LightSwitchController _kitSwitch;
        private LightSwitchController _bedSwitch;

        private OnOffController _tv;
        private OnOffController _bedroomCrt;
        private OnOffController _kitchenCrt;

        private bool _tvIsOn;
        private bool _bedroomCrtIsOn;
        private bool _kitchenCrtIsOn;

        private BreakerController _breaker;

        private List<LightSwitchController> _controllers;

        // a lot of initialization...
        void Start()
        {

            _breaker = breakerContainer.GetComponent<BreakerController>();

            _controllers = new List<LightSwitchController>();

            _livSwitch = livSwitchContainer.GetComponent<LightSwitchController>();
            _foySwitch = foySwitchContainer.GetComponent<LightSwitchController>();
            _batSwitch = batSwitchContainer.GetComponent<LightSwitchController>();
            _kitSwitch = kitSwitchContainer.GetComponent<LightSwitchController>();
            _bedSwitch = bedSwitchContainer.GetComponent<LightSwitchController>();

            _tv = tvContainer.GetComponent<OnOffController>();
            _bedroomCrt = bedroomCrtContainer.GetComponent<OnOffController>();
            _kitchenCrt = kitchenCrtContainer.GetComponent<OnOffController>();

            _controllers.Add(_livSwitch);
            _controllers.Add(_foySwitch);
            _controllers.Add(_batSwitch);
            _controllers.Add(_kitSwitch);
            _controllers.Add(_bedSwitch);

            _tvIsOn = _tv.GetIsOn();
            _bedroomCrtIsOn = _bedroomCrt.GetIsOn();
            _kitchenCrtIsOn = _kitchenCrt.GetIsOn();
        }

        // Update is called once per frame
        void Update()
        {
            // a hopefully low performance impact approach to monitoring interactables
            if (_tvIsOn && _bedroomCrtIsOn && _kitchenCrtIsOn) KillPower();
        }

        public bool GetPowerIsOn()
        {
            return isOn;
        }

        // Externally callable method to set tracked state of electronic device
        public void UpdateElectronicDevice(string deviceContainer, bool newState)
        {
            if (deviceContainer.Equals(tvContainer.name)) _tvIsOn = newState;
            if (deviceContainer.Equals(bedroomCrtContainer.name)) _bedroomCrtIsOn = newState;
            if (deviceContainer.Equals(kitchenCrtContainer.name)) _kitchenCrtIsOn = newState;
        }

        public void KillPower()
        {
            Debug.Log("Test: Killing power!");
            if (deactivateSound)
                AudioUtility.CreateSFX(deactivateSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);

            foreach (var c in _controllers)
            {
                c.ForceOff();
            }

            _breaker.ForceOff();
            isOn = false;
        }

        public void RestorePower()
        {
            if (activateSound)
                AudioUtility.CreateSFX(activateSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
            isOn = true;
        }
    }
}
