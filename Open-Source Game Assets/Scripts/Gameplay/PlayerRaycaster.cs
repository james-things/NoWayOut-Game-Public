using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace NoWayOut.Scripts.Gameplay
{
    public class PlayerRaycaster : MonoBehaviour
    {
        [SerializeField] private Image crosshairImage;
        public float range = 2;
        public GameObject player;

        private Image _crosshairImage;

        void Start()
        {
            _crosshairImage = crosshairImage;
        }

        // Update is called once per frame
        void Update()
        {
            CastRay();
        }

        // Ray-casting hit detection
        void CastRay()
        {
            // fire ray at target range and return hit info
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, range);

            // if hit, fetch hit game object
            if (hit)
            {
                GameObject hitObject = hitInfo.transform.gameObject;
                
                // try to grab interactor
                IInteractable interactor = hitObject.GetComponent<IInteractable>();
                
                // if an interactor is found
                if (interactor != null)
                {
                    // set cross-hair to green
                    _crosshairImage.color = Color.green;

                    // if M1 was pressed this frame, call interaction
                    if (Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame)
                    {
                        hitObject.GetComponent<IInteractable>().Interact(player.GetComponent<Interactor>());
                    }
                }
                // else it is not an interactable
                else
                {
                    // however it may be a pin button, which does not use Interactor
                    PinButtonController pbc = hitObject.GetComponent<PinButtonController>();
                    // if we found a pinButtonController, set cross-hair green
                    if (pbc != null) _crosshairImage.color = Color.green;
                    // else set cross-hair to white
                    else _crosshairImage.color = Color.white;
                }
            }
            else // not a hit
            {
                // turn cross-hair to white
                _crosshairImage.color = Color.white;
            }
        }
    }
}
