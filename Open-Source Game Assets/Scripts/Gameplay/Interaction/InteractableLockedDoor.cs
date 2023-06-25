using DG.Tweening;
using NoWayOut.Scripts.Game;
using UnityEngine;

namespace NoWayOut.Scripts.Gameplay
{
    public class InteractableLockedDoor : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _tweenID;
        [SerializeField] private GameObject _player;
        [SerializeField] private AudioClip lockedSound;
        [SerializeField] private AudioClip openSound;

        private TimeLimit _timeLimit;
        private bool _isOpen = false;

        void Start()
        {
            // for logging
            _timeLimit = _player.GetComponent<TimeLimit>();
        }

        public bool Interact(Interactor interactor)
        {
            Debug.Log("Interaction: " + name + " " + "[locked!]" + " at " + _timeLimit.timeLeftString + ".");
            if (!_isOpen)
            {
                PlayLockedSound();
            }

            return true;
        }

        private void PlayLockedSound()
        {
            if (lockedSound)
                AudioUtility.CreateSFX(lockedSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);

        }

        public void UnlockAndOpen()
        {
            if (!_isOpen)
            {
                DOTween.Play(_tweenID);
                _isOpen = true;

                if (openSound)
                    AudioUtility.CreateSFX(openSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
            }
        }
    }
}
