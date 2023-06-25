using UnityEngine;

// Controller for Ceiling Point Light
// "Subscribes" to state of light switch -James
namespace NoWayOut.Scripts.Gameplay
{
    public class LightController : MonoBehaviour
    {
        [SerializeField] private Transform pointLight;
        [SerializeField] private Transform controllingSwitch;

        private bool isOn;
        private LightSwitchController _controllingSwitch;
        private Light _lightP;

        // Grab controllers and initialize state
        void Start()
        {
            _controllingSwitch = controllingSwitch.GetComponent<LightSwitchController>();
            _lightP = pointLight.GetComponent<Light>();

            isOn = _controllingSwitch.GetIsOn();

            if (isOn)
            {
                _lightP.enabled = true;
            }
            else
            {
                _lightP.enabled = false;
            }
        }

        // "Subscription" to switch
        void Update()
        {
            if (_controllingSwitch.GetIsOn())
            {
                ForceOn();
            }
            else
            {
                ForceOff();
            }
        }

        public void ForceOff()
        {
            if (isOn) ToggleLight();
        }

        public void ForceOn()
        {
            if (!isOn) ToggleLight();
        }

        public bool GetIsOn()
        {
            return isOn;
        }

        // Update is called once per frame
        public void ToggleLight()
        {
            if (isOn)
            {
                _lightP.enabled = false;
                isOn = false;
            }
            else
            {
                _lightP.enabled = true;
                isOn = true;
            }
        }
    }
}
