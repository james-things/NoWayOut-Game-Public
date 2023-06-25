using NoWayOut.Scripts.Game;
using NoWayOut.Scripts.Global;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NoWayOut.Scripts.UI
{
    public class HUDVolController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI volText;
        
        private TextMeshProUGUI _volText;

        // Start is called before the first frame update
        void Start()
        {
            _volText = volText;
        }

        // Update is called once per frame
        void Update()
        {
            // After change, this corrects HUD next frame 
            _volText.text = GlobalVariables.Get<int>("volume").ToString();

            if (Keyboard.current.numpadPlusKey.wasPressedThisFrame || Keyboard.current.equalsKey.wasPressedThisFrame)
            {
                InGameIncrementVolume();
            }

            if (Keyboard.current.numpadMinusKey.wasPressedThisFrame || Keyboard.current.minusKey.wasPressedThisFrame)
            {
                InGameDecrementVolume();
            }
            
            // try to ensure volume is correctly set - this is expensive and a temporary way until the issue is found!
            if ((int)AudioUtility.GetMasterVolume() * 10 != GlobalVariables.Get<int>("volume"))
            {
                AudioUtility.SetMasterVolume((float)GlobalVariables.Get<int>("volume") / 10);
            }
        }

        private void InGameIncrementVolume()
        {
            int curVolSetting = int.Parse(_volText.text);
            if (curVolSetting != 10)
            {
                int newVolSetting = curVolSetting + 1;
                GlobalVariables.Set("volume", newVolSetting);
                AudioUtility.SetMasterVolume((float)newVolSetting / 10);

                Debug.Log("incremented volume");
            }
        }

        private void InGameDecrementVolume()
        {
            int curVolSetting = int.Parse(_volText.text);
            if (curVolSetting != 0)
            {
                int newVolSetting = curVolSetting - 1;
                GlobalVariables.Set("volume", newVolSetting);
                AudioUtility.SetMasterVolume((float)newVolSetting / 10);

                Debug.Log("decremented volume");
            }
        }
    }
}
