using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private Transform rayCastPoint;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private GameObject interactUI;
    [SerializeField] private Button interactButton;


    public bool CanInteract;

    private Camera cam;
    private IInteractable currentInteractable;

    private void Start()
    {
        cam = Camera.main;
        interactUI.SetActive(false); // Hide UI at start

        // Subscribe a listener function to the button's onClick event
        if (interactButton != null)
        {
            interactButton.onClick.AddListener(HandleInteractButtonClick);
        }
        else
        {
            Debug.LogError("Interact Button is not assigned in the Inspector!");
        }
    }

    private void Update()
    {
        if (CanInteract)
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
            {
                currentInteractable = hit.collider.GetComponent<IInteractable>();

                if (currentInteractable != null)
                {
                    interactUI.SetActive(true);

                    if (Input.GetKeyDown(interactKey))
                    {
                        currentInteractable.Interact();
                    }
                    return; // Important: Exit Update after finding and potentially interacting
                }
            }

            // If no interactable was hit or found
            currentInteractable = null;
            interactUI.SetActive(false);
        }
        else
        {
            currentInteractable = null;
            interactUI.SetActive(false);
        }
        

    }

    // This function will be called when the interact button is clicked
    private void HandleInteractButtonClick()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
}