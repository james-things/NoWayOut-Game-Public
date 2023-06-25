using DG.Tweening;
using NoWayOut.Scripts.Game;
using UnityEngine;

// generified version of original door script
namespace NoWayOut.Scripts.Gameplay
{
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _tweenID;
        [SerializeField] private GameObject _player;
        [SerializeField] private AudioClip activateSound;
        [SerializeField] private AudioClip deactivateSound;

        private TimeLimit _timeLimit;

        private bool _isOpen = false;

        void Start()
        {
            // for logging
            _timeLimit = _player.GetComponent<TimeLimit>();
        }

        public bool Interact(Interactor interactor)
        {
            string stateTransition = _isOpen ? "[open -> close]" : "[close -> open]";
            Debug.Log("Interaction: " + name + " " + stateTransition + " at " + _timeLimit.timeLeftString + ".");
            ToggleInteractable();
            return true;
        }

        private void ToggleInteractable()
        {
            if (_isOpen)
            {
                DOTween.PlayBackwards(_tweenID);
                _isOpen = false;

                if (deactivateSound)
                    AudioUtility.CreateSFX(deactivateSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
            }
            else
            {
                DOTween.Restart(_tweenID);
                DOTween.Play(_tweenID);
                _isOpen = true;

                if (activateSound)
                    AudioUtility.CreateSFX(activateSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);

            }
        }
    }
}