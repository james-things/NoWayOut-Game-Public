using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NoWayOut.Scripts.Game;
using UnityEngine;

namespace NoWayOut.Scripts.Gameplay
{
    public class BreakerController : MonoBehaviour, IInteractable
    {
        [SerializeField] private string tweenID;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject powerController;

        private bool _isOn;
        private TimeLimit _timeLimit;
        private LightSwitchController _controllingSwitch;
        private GameObject _controlledObject;
        private PowerController _powerController;

        // Grab controller and intialize state
        void Start()
        {
            _powerController = powerController.GetComponent<PowerController>();
            _isOn = _powerController.GetPowerIsOn();

            if (!_isOn)
            {
                Debug.Log("found breaker off init");
                DOTween.Play("breaker_off");
            }

            // for logging
            _timeLimit = player.GetComponent<TimeLimit>();
        }


        public void ForceOff()
        {
            if (_isOn)
            {
                _isOn = false;
                DOTween.Restart(tweenID);
                DOTween.Play(tweenID);
            }
        }

        public bool Interact(Interactor interactor)
        {
            string stateTransition = _isOn ? "[on -> off]" : "[off -> on]";
            Debug.Log("Interaction: " + name + " " + stateTransition + " at " + _timeLimit.timeLeftString + ".");

            ToggleBreaker();

            return true;
        }

        private void ToggleBreaker()
        {
            if (_isOn)
            {
                _isOn = false;
                DOTween.Restart(tweenID);
                DOTween.Play(tweenID);
                _powerController.KillPower();
            }
            else
            {
                _isOn = true;
                DOTween.PlayBackwards(tweenID);
                _powerController.RestorePower();
            }
        }
    }
}
