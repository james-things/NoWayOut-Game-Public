using NoWayOut.Scripts.Game;
using NoWayOut.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NoWayOut.Scripts.UI
{
    public class InGameMenuManager : MonoBehaviour
    {
        [Tooltip("Root GameObject of the menu used to toggle its activation")]
        public GameObject MenuRoot;

        private float VolumeWhenMenuOpen = 0f;
        /*
        [Tooltip("Slider component for look sensitivity")]
        public Slider LookSensitivitySlider;

        [Tooltip("Toggle component for shadows")]
        public Toggle ShadowsToggle;

        [Tooltip("GameObject for the controls")]
        public GameObject ControlImage;
        */
        PlayerInputHandler m_PlayerInputsHandler;
        // Health m_PlayerHealth;

        void Start()
        {
            m_PlayerInputsHandler = FindObjectOfType<PlayerInputHandler>();
            DebugUtility.HandleErrorIfNullFindObject<PlayerInputHandler, InGameMenuManager>(m_PlayerInputsHandler,
                this);
            /*
            m_PlayerHealth = m_PlayerInputsHandler.GetComponent<Health>();
            DebugUtility.HandleErrorIfNullGetComponent<Health, InGameMenuManager>(m_PlayerHealth, this, gameObject);

            MenuRoot.SetActive(false);

            LookSensitivitySlider.value = m_PlayerInputsHandler.LookSensitivity;
            LookSensitivitySlider.onValueChanged.AddListener(OnMouseSensitivityChanged);

            ShadowsToggle.isOn = QualitySettings.shadows != ShadowQuality.Disable;
            ShadowsToggle.onValueChanged.AddListener(OnShadowsChanged);
            */
        }

        void Update()
        {
            // Lock cursor when clicking outside of menu
            if (!MenuRoot.activeSelf && Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            
            if (Keyboard.current.pKey.wasPressedThisFrame || Keyboard.current.tabKey.wasPressedThisFrame) TogglePauseMenu();

            if (MenuRoot.activeSelf && Keyboard.current.mKey.wasPressedThisFrame)
            {
                TogglePauseMenu();
                SceneManager.LoadScene("IntroMenu");
            }
            
            if (MenuRoot.activeSelf && Keyboard.current.xKey.wasPressedThisFrame) Application.Quit();
            
            /*
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Cursor.lockState = CursorLockMode.None;
                // Cursor.visible = true;
            }

            if (Input.GetButtonDown(GameConstants.k_ButtonNamePauseMenu)
                || (MenuRoot.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel)))
            {
                SetPauseMenuActivation(!MenuRoot.activeSelf);
            }
            */
        }

        private void TogglePauseMenu()
        {
            if (!MenuRoot.activeSelf)
            {
                SetPauseMenuActivation(true);
            }
            else
            {
                SetPauseMenuActivation(false);
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
                // Cursor.visible = true;
                Time.timeScale = 0f;
                AudioUtility.SetMasterVolume(VolumeWhenMenuOpen);

                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                // Cursor.visible = false;
                Time.timeScale = 1f;
                AudioUtility.SetMasterVolume(1);
            }

        }

        void OnMouseSensitivityChanged(float newValue)
        {
            m_PlayerInputsHandler.LookSensitivity = newValue;
        }

        void OnShadowsChanged(bool newValue)
        {
            QualitySettings.shadows = newValue ? ShadowQuality.All : ShadowQuality.Disable;
        }
    }
}