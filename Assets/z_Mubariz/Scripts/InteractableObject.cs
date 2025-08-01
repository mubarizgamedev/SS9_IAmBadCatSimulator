using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] RawImage m_RawImage;

    public void Interact()
    {
        Debug.Log("Interacted with: " + gameObject.name);
        
    }
}
