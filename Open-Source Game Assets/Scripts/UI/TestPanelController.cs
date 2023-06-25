using NoWayOut.Scripts.Game;
using NoWayOut.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NoWayOut.Scripts.UI
{
    public class TestPanelController : MonoBehaviour
    {
        [Tooltip("Root GameObject of the menu used to toggle its activation")]
        public GameObject MenuRoot;

        [Tooltip("Master volume when menu is open")] [Range(0.001f, 1f)]
        public float VolumeWhenMenuOpen = 0.5f;

        PlayerInputHandler m_PlayerInputsHandler;

        void Start()
        {
            m_PlayerInputsHandler = FindObjectOfType<PlayerInputHandler>();
            DebugUtility.HandleErrorIfNullFindObject<PlayerInputHandler, TestPanelController>(m_PlayerInputsHandler,
                this);
        }

        void Update()
        {
            if (!MenuRoot.activeSelf && Keyboard.current.tKey.wasPressedThisFrame)
            {
                SetMenuActivation(true);
                Debug.Log("Activate Menu");
            }

            if (MenuRoot.activeSelf && Keyboard.current.tKey.wasPressedThisFrame)
            {
                SetMenuActivation(false);
                Debug.Log("Dectivate Menu");
            }
            // Lock cursor when clicking outside of menu
            // if (!MenuRoot.activeSelf && Input.GetMouseButtonDown(0))
            // {
            //     Cursor.lockState = CursorLockMode.Locked;
            //     Cursor.visible = false;
            // }

            // if (Input.GetKeyDown(KeyCode.Escape))
            // {
            //     Cursor.lockState = CursorLockMode.None;
            //     Cursor.visible = true;
            // }

            // if (Input.GetKeyDown(KeyCode.I)
            //     || (MenuRoot.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel)))
            // {
            //     SetPauseMenuActivation(!MenuRoot.activeSelf);
            // }
        }


        private void SetMenuActivation(bool state)
        {
            if (MenuRoot.activeSelf)
            {
                // Cursor.lockState = CursorLockMode.None;
                // Cursor.visible = true;
                Time.timeScale = 0f;
                AudioUtility.SetMasterVolume(VolumeWhenMenuOpen);
                MenuRoot.SetActive(state);
                // EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                // Cursor.lockState = CursorLockMode.Locked;
                // Cursor.visible = false;
                Time.timeScale = 1f;
                AudioUtility.SetMasterVolume(1);
                MenuRoot.SetActive(state);
            }

        }
    }
}
