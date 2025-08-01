using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRaycaster : MonoBehaviour
{
    [Header("Testing")]
    public bool objectFound;
    public bool enemyFound;


    /////////////////////////////////// 
    /// 
    ///   RAYCASTING
    /// 
    ///////////////////////////////////

    [Header("RayCasting")]
    [SerializeField] float playerRayCastRange;
    [SerializeField] Transform rayCastOrigin;
    [SerializeField] LayerMask interactibleLayer;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask rewardedLayer;
    [SerializeField] GameObject rewardedObject;
    




    /////////////////////////////////// 
    /// 
    ///     EVENTS
    /// 
    ///////////////////////////////////
    [Header("Events")]
    public UnityEvent OnInteractWithPickable;
    public UnityEvent OnInteractWithGranny;
    public UnityEvent NotInteractWithPickable;
    public UnityEvent NotInteractWithGranny;
    public static event Action OnObjectInWorldHide;
    public event EventHandler<OnInteractedWithRewardedClass> OnInteractedWithRewarded;

    public class OnInteractedWithRewardedClass : EventArgs
    {
        public GameObject rewardedGameObject;
    }



    /////////////////////////////////// 
    /// 
    ///     FIELDS
    /// 
    ///////////////////////////////////


    RaycastHit hit;
    public GameObject currentPickedObjet;
    public string currentObjectName;



    #region UNITY FUNCTIONS

    private void Start()
    {
      
            
        ObjectThrower.OnObjectThrown += ObjectThrower_OnObjectThrown;
        ObjectPicker.OnPickButtonClick += ObjectPicker_OnPickButtonClick;
        PickableObject.OnObjectReachedPlayer += PickableObject_OnObjectReachedPlayer;
    }

    private void OnDestroy()
    {
        ObjectThrower.OnObjectThrown -= ObjectThrower_OnObjectThrown;
        ObjectPicker.OnPickButtonClick -= ObjectPicker_OnPickButtonClick;
        PickableObject.OnObjectReachedPlayer -= PickableObject_OnObjectReachedPlayer;
    }
    private void ObjectThrower_OnObjectThrown()
    {
        currentObjectName = null;
        currentPickedObjet = null;
    }

    private void Update()
    {
        RayCasting_Object();
        RayCasting_Granny();
        CheckForRewarded();
        //DrawDebugRay();
    }
    #endregion


    #region EVENT SUBSCRIBING
    private void PickableObject_OnObjectReachedPlayer()
    {
        HideInteractedGameObject();
    }

    private void ObjectPicker_OnPickButtonClick()
    {
        MoveObjectTowardPlayer();
    }
    #endregion


    #region HELPER FUNCTIONS

    void RayCasting_Object()
    {
        if (SpecialItemInHand.Instance.handFreeAtMoment)
        {
            if (ObjectPicker.canGrabObject)   // IF THERE IS NOT ALREADY ANY OBJECT IN HAND
            {
                Ray ray = new Ray(rayCastOrigin.position, transform.forward);
                RaycastHit[] hits = Physics.RaycastAll(ray, playerRayCastRange);

                // Sort hits by distance (closest first)
                Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

                foreach (RaycastHit hitInfo in hits)
                {
                    // If we hit the floor first, stop raycast
                    if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
                    {
                        NotInteractWithPickable?.Invoke();
                        return; // Stop checking further objects
                    }

                    // If the hit is an interactable object
                    if (((1 << hitInfo.collider.gameObject.layer) & interactibleLayer) != 0)
                    {
                        objectFound = true;
                        OnInteractWithPickable?.Invoke();
                        //Debug.Log("interectable found found");
                        if (hitInfo.collider.TryGetComponent<PickableObject>(out PickableObject pickableObject))
                        {
                            currentObjectName = pickableObject.GetObjectName();
                            currentPickedObjet = hitInfo.transform.gameObject;
                        }
                        return; // Stop after first valid interactable hit
                    }
                }

                // If no interactable object was found, invoke "not interacting" event
                NotInteractWithPickable?.Invoke();
            }
        }        
    }



    void CheckForRewarded()
    {
        if (SpecialItemInHand.Instance.handFreeAtMoment)
        {
            if (ObjectPicker.canGrabObject)   // IF THERE IS NOT ALREADY ANY OBJECT IN HAND
            {
                Ray ray = new Ray(rayCastOrigin.position, transform.forward);
                RaycastHit[] hits = Physics.RaycastAll(ray, playerRayCastRange);

                // Sort hits by distance (closest first)
                Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

                foreach (RaycastHit hitInfo in hits)
                {
                    // If the first hit is the floor, stop checking further
                    if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
                    {
                        rewardedObject.SetActive(false); // Hide reward UI
                        return; // Stop checking further objects
                    }

                    // If the hit is a rewarded object
                    if (((1 << hitInfo.collider.gameObject.layer) & rewardedLayer) != 0)
                    {
                        rewardedObject.SetActive(true);
                        OnInteractedWithRewarded?.Invoke(this, new OnInteractedWithRewardedClass
                        {
                            rewardedGameObject = hitInfo.collider.gameObject
                        });
                        return; // Stop after first valid rewarded object hit
                    }
                }

                // If no reward object was found, hide the UI
                rewardedObject.SetActive(false);
            }
        }            
    }


    

    public string InteractedObjectName()
    {
        return currentObjectName;
    }

     
    //CALL METHOD (Moveobject_Towardplayer) OF PICKABEL OBJECT CLASS
    void MoveObjectTowardPlayer()
    {
        if (objectFound && currentPickedObjet != null) // Ensure Raycast hit an object
        {
            if (currentPickedObjet.TryGetComponent(out PickableObject pickable))
            {
                pickable.MoveObject_TowardPlayer();
            }
        }
        else
        {
            Debug.LogWarning("No interactable object found in front of the player.");
        }
    }


    //HIDE OBJECT PRESENT IN WORLD AND SEND A CALL 
    void HideInteractedGameObject()
    {
        
        if (currentPickedObjet != null)
        {
            currentPickedObjet.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No object found to hide.");
        }
        
        OnObjectInWorldHide?.Invoke();
    }
    #endregion

    void DrawDebugRay()
    {
        Debug.DrawRay(rayCastOrigin.position, transform.forward * playerRayCastRange, Color.red);
    }

    void RayCasting_Granny()
    {
        Ray ray = new Ray(rayCastOrigin.position, transform.forward);
        if (Physics.Raycast(ray, out hit, playerRayCastRange))
        {
            string layerName = LayerMask.LayerToName(hit.collider.gameObject.layer);
            //Debug.Log("HIT: " + hit.collider.name + " on layer " + layerName);

            if (layerName == "Granny")
            {
                enemyFound = true;
                OnInteractWithGranny?.Invoke();
                //Debug.Log("Enemy Found: Granny detected!");
            }
            else
            {
                enemyFound = false;
                NotInteractWithGranny?.Invoke();
            }
        }
        else
        {
            enemyFound = false;
        }
    }

}
