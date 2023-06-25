using UnityEngine;

namespace NoWayOut.Scripts.Gameplay
{
    public class PinButtonController : MonoBehaviour
    {
        public string value;
        public PinDisplayController _pinPadDisplay;

        // Handle buttons that add to the equation
        public void HandleEntry()
        {
            _pinPadDisplay.UpdateDisplayText(value);
        }

        // Handle equals button
        public void HandleSubmit()
        {
            _pinPadDisplay.ProcessSubmit();
        }

        // Handle clear button
        public void HandleReset()
        {
            _pinPadDisplay.ResetDisplay();
        }
    }
}