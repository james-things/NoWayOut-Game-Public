using System;
using NoWayOut.Scripts.Game;
using NoWayOut.Scripts.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NoWayOut.Scripts.UI
{
    public class PlayerTimeBar : MonoBehaviour
    {
        [Tooltip("Image component dispplaying current time left")]
        public Image FillImageTime;

        public TextMeshProUGUI LimitCounterText;

        TimeLimit m_TimeLimit;

        void Start()
        {
            PlayerCharacterController playerCharacterController = GameObject.FindObjectOfType<PlayerCharacterController>();
            DebugUtility.HandleErrorIfNullFindObject<PlayerCharacterController, PlayerTimeBar>(playerCharacterController, this);

            m_TimeLimit = playerCharacterController.GetComponent<TimeLimit>();
            DebugUtility.HandleErrorIfNullGetComponent<TimeLimit, PlayerTimeBar>(m_TimeLimit, this, playerCharacterController.gameObject);
        }

        void Update()
        {
            var ts = TimeSpan.FromSeconds(m_TimeLimit.timeLeft);
            LimitCounterText.text = $"{ts.Minutes:00}:{ts.Seconds:00}";
            // update time bar value
            FillImageTime.fillAmount = m_TimeLimit.timeLeft / m_TimeLimit.timeLimit;
        }
    }
}