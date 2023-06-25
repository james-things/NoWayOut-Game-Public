using UnityEngine;
using UnityEngine.InputSystem;

// Adapted from https://www.youtube.com/watch?v=THmW4YolDok -James
namespace NoWayOut.Scripts.Gameplay
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Transform interactionPoint;
        [SerializeField] private float interactionPointRadius = 0.5f;
        [SerializeField] private LayerMask interactableMask;

        private readonly Collider[] _colliders = new Collider[3];
        [SerializeField] private int numFound;

        private void Update()
        {
            // find the first 3 colliders in the interactable layer at the interaction point
            numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, _colliders,
                interactableMask);

            // if colliders found, invoke interfaces
            if (numFound > 0)
            {
                var interactable = _colliders[0].GetComponent<IInteractable>();

                // if (interactable != null && Keyboard.current.eKey.wasPressedThisFrame)
                if (interactable != null && Mouse.current.leftButton.wasPressedThisFrame)
                {
                    interactable.Interact(this);
                }
            }
        }
    }
}