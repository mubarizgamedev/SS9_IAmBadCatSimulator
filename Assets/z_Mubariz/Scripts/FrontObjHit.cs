using UnityEngine;

public class FrontObjHit : MonoBehaviour
{
    public float checkDistance = 5f;
    public bool checkFront = false;

    public Transform raycastOrigin; // NEW: Assign in Inspector (e.g., camera, hand, etc.)
    private PickableObject detectedPickable;

    public LayerMask interactableLayers;

    bool objectInFront = false;

    public GameObject hitUIGameobject;
    public float waitTime = 1f;

    private void Start()
    {
        CatScratch.OnScratchButtonClickedEvent += CatScratch_OnScratchButtonClickedEvent;
    }
    private void OnDestroy()
    {
        CatScratch.OnScratchButtonClickedEvent -= CatScratch_OnScratchButtonClickedEvent;
    }
    

    void Update()
    {
        CheckForObjectInFront();
        CheckObject();

        
    }

    void CheckForObjectInFront()
    {
        if (raycastOrigin == null)
        {
            Debug.LogWarning("Raycast origin not set.");
            objectInFront = false;
            return;
        }

        RaycastHit hit;

        // Cast the ray from the raycastOrigin
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, checkDistance, interactableLayers))
        {
            PickableObject pickable = hit.collider.GetComponent<PickableObject>();

            if (pickable != null)
            {
                detectedPickable = pickable;
                objectInFront = true;
            }
            else
            {
                objectInFront = true;
                detectedPickable = null;
            }
        }
        else
        {
            objectInFront = false;
            detectedPickable = null;
        }
    }

    public PickableObject GetDetectedPickable()
    {
        return detectedPickable;
    }

    // 👁️ Draw a visual ray in the Scene view
    void OnDrawGizmos()
    {
        if (raycastOrigin != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(raycastOrigin.position, raycastOrigin.position + raycastOrigin.forward * checkDistance);
        }
    }

    void CheckObject()
    {
        hitUIGameobject.SetActive(objectInFront);
    }

    private void CatScratch_OnScratchButtonClickedEvent()
    {
        if(GetDetectedPickable() != null)
        GetDetectedPickable().PushObject(waitTime);
    }
}
