using System;
using NoWayOut.Scripts.Game;
using NoWayOut.Scripts.Global;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NoWayOut.Scripts.UI
{
    public class NotificationHUDManager : MonoBehaviour
    {
        [Tooltip("UI panel containing the layoutGroup for displaying notifications")]
        public RectTransform NotificationPanel;

        [Tooltip("Prefab for the notifications")]
        public GameObject NotificationPrefab;

        private int _hintCount = 1;
        
        void Awake()
        {
            //EventManager.AddListener<ObjectiveUpdateEvent>(OnObjectiveUpdateEvent);
        }

        private void Update()
        {
            // CreateNotification("This is a test hint!");
            if (Keyboard.current.hKey.wasPressedThisFrame)
            {
                if (GlobalVariables.Get<string>("difficulty").Equals("hard"))
                {
                    ShowHardHint();
                }
                else
                {
                    ShowEasyHint();
                }
            }
        }

        private void CyclicalAdvanceHintCount()
        {
            if (_hintCount == 5) _hintCount = 1;
            else _hintCount += 1;
        }
        
        private void ShowHardHint()
        {
            switch (_hintCount)
            {
                case 1: 
                    CreateNotification("There is useful information on the screens.");
                    break;
                case 2: 
                    CreateNotification("The light switches are key to solving the puzzle this time.");
                    break;
                case 3: 
                    CreateNotification("If the power goes out, you'll have to find the breaker.");
                    break;
                case 4: 
                    CreateNotification("The power for each room is controlled by a switch.");
                    break;
                case 5: 
                    CreateNotification("When the breaker shuts off, so do the switches.");
                    break;
            }
            CyclicalAdvanceHintCount();
        }

        private void ShowEasyHint()
        {
            switch (_hintCount)
            {
                case 1: 
                    CreateNotification("The cursor will appear green to indicate an object is interactable.");
                    break;
                case 2: 
                    CreateNotification("Interactions have a limited range, be sure you get close enough to things!");
                    break;
                case 3: 
                    CreateNotification("The goal is to work out a sequence, consider this as you explore!");
                    break;
                case 4: 
                    CreateNotification("If you reset the power, you will need to turn at least one light switch back on afterwards.");
                    break;
                case 5: 
                    CreateNotification("There is a very important clue on the kitchen table.");
                    break;
            }
            CyclicalAdvanceHintCount();
        }
        /*
        void OnObjectiveUpdateEvent(ObjectiveUpdateEvent evt)
        {
            if (!string.IsNullOrEmpty(evt.NotificationText))
                CreateNotification(evt.NotificationText);
        }
        */
        public void CreateNotification(string text)
        {
            GameObject notificationInstance = Instantiate(NotificationPrefab, NotificationPanel);
            notificationInstance.transform.SetSiblingIndex(0);

            NotificationToast toast = notificationInstance.GetComponent<NotificationToast>();
            if (toast)
            {
                toast.Initialize(text);
            }
        }

        //void OnDestroy()
        //{
        //    EventManager.RemoveListener<ObjectiveUpdateEvent>(OnObjectiveUpdateEvent);
        //}
    }
}