using NoWayOut.Scripts.Game;
using NoWayOut.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NoWayOut.Scripts.UI
{
    public class InventoryPanelManager : MonoBehaviour
    {
        [Tooltip("Root GameObject of the menu used to toggle its activation")]
        public GameObject MenuRoot;

        [Tooltip("Master volume when menu is open")] [Range(0.001f, 1f)]
        public float VolumeWhenMenuOpen = 0.5f;

        PlayerInputHandler m_PlayerInputsHandler;

        void Start()
        {
            m_PlayerInputsHandler = FindObjectOfType<PlayerInputHandler>();
            DebugUtility.HandleErrorIfNullFindObject<PlayerInputHandler, InventoryPanelManager>(m_PlayerInputsHandler,
                this);
            
            MenuRoot.SetActive(false);
        }

        void Update()
        {
            // Lock cursor when clicking outside of menu
            if (!MenuRoot.activeSelf && Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetKeyDown(KeyCode.I)
                || (MenuRoot.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel)))
            {
                SetPauseMenuActivation(!MenuRoot.activeSelf);
            }
        }

        public void ClosePauseMenu()
        {
            SetPauseMenuActivation(false);
        }

        void SetPauseMenuActivation(bool active)
        {
            MenuRoot.SetActive(active);

            if (MenuRoot.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
                AudioUtility.SetMasterVolume(VolumeWhenMenuOpen);

                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                AudioUtility.SetMasterVolume(1);
            }

        }
    }
}