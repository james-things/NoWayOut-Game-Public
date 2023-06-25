using TMPro;
using UnityEngine;

// Class to handle initialization of main scene, setting difficulty
namespace NoWayOut.Scripts.Gameplay
{
    public class DifficultyManager : MonoBehaviour
    {
        [SerializeField] private int easyModePin;
        [SerializeField] private int hardModePin;
        [SerializeField] private GameObject pinDisplayContainer;
        [SerializeField] private TextMeshPro tvDisplayText;
        [SerializeField] private GameObject bedroomCrtContainer;
        [SerializeField] private GameObject kitchenCrtContainer;
        [SerializeField] private GameObject hintShapes;
        [SerializeField] private GameObject tvContainer;

        private PinDisplayController _pinDisplay;
        private MeshRenderer _tvText;
        private GameObject _bedroomCrt;
        private GameObject _kitchenCrt;
        private GameObject _hintShapes;
        private OnOffController _tvController;
        

        public void ApplyEasyMode()
        {
            _pinDisplay = pinDisplayContainer.GetComponent<PinDisplayController>();
            _tvText = tvDisplayText.GetComponent<MeshRenderer>();
            _bedroomCrt = bedroomCrtContainer; 
            _kitchenCrt = kitchenCrtContainer; 
            _hintShapes = hintShapes;
            _tvController = tvContainer.GetComponent<OnOffController>();

            _pinDisplay.SetPinCode(easyModePin);
            _tvText.enabled = false;
            _bedroomCrt.SetActive(false);
            _kitchenCrt.SetActive(false);
            _hintShapes.SetActive(true);
            if (!_tvController.GetIsOn()) _tvController.TurnOn();
            Debug.Log("Easy mode initialization applied");
        }

        public void ApplyHardMode()
        {
            _pinDisplay = pinDisplayContainer.GetComponent<PinDisplayController>();
            _tvText = tvDisplayText.GetComponent<MeshRenderer>();
            _bedroomCrt = bedroomCrtContainer; 
            _kitchenCrt = kitchenCrtContainer; 
            _hintShapes = hintShapes;
            _tvController = tvContainer.GetComponent<OnOffController>();

            _pinDisplay.SetPinCode(hardModePin);
            _tvText.enabled = true;
            _bedroomCrt.SetActive(true);
            _kitchenCrt.SetActive(true);
            _hintShapes.SetActive(false);
            if (_tvController.GetIsOn()) _tvController.ForceOff();
            Debug.Log("Hard mode initialization applied");
        }
    }
}
