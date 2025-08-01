using System;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPicker : MonoBehaviour
{
    public static bool canGrabObject;
    [SerializeField] PlayerRaycaster playerRaycaster;
    [SerializeField] AudioClip pickSound;
    [SerializeField] GameObject rewardedObject;
    [SerializeField] GameObject greenCrosshair;
    public static event Action OnGuideObjectPicked;

    /////////////////////////////////// 
    /// 
    ///    EVENTS
    /// 
    ///////////////////////////////////


    public static event Action OnPickButtonClick;
    public UnityEvent OnObjectGathered;
    public static event Action ObjectPicked;


    /////////////////////////////////// 
    /// 
    ///    STRUCT TO STORE OBJECT NAME AND REF
    /// 
    ///////////////////////////////////

    [System.Serializable]
    public class PickAbleObject
    {
        public string ObjectNameInSO;
        public GameObject ObjectInPlayerHand;
    }
    public PickAbleObject[] pickAbleObject;


    #region UNITY FUNCTIONS
    private void Start()
    {
       
        canGrabObject = true;
        PlayerRaycaster.OnObjectInWorldHide += PlayerRaycaster_OnObjectInWorldHide;
    }

    private void OnDestroy()
    {
        PlayerRaycaster.OnObjectInWorldHide -= PlayerRaycaster_OnObjectInWorldHide;
    }
    #endregion



    #region EVENT SUBSCRIBERS
    private void PlayerRaycaster_OnObjectInWorldHide()
    {
        Enable_Corresponding_Object_At_Player();
    }
    #endregion


    #region HELPER FUNTIONS
    public void PickObject()
    {
        canGrabObject = false;
        if (pickSound != null)
        {
            SFX_Manager.PlaySound(pickSound);
        }
        OnPickButtonClick?.Invoke();
        if (rewardedObject.activeSelf == true)
        {
            rewardedObject.SetActive(false);
        }
        if (greenCrosshair.activeSelf == true)
        {
            greenCrosshair.SetActive(false);
        }
    }

    void Enable_Corresponding_Object_At_Player()
    {
        Debug.Log("Current object name :" + CurrentObjectName());
        foreach (var objectInfo in pickAbleObject)
        {
           
            if (objectInfo.ObjectNameInSO == CurrentObjectName())
            {
                if(objectInfo.ObjectInPlayerHand == null)
                {
                    Debug.LogWarning("ObjectInPlayerHand is not assigned for " + objectInfo.ObjectNameInSO);
                    return;
                }

                objectInfo.ObjectInPlayerHand.SetActive(true);
                OnGuideObjectPicked?.Invoke();
                canGrabObject = false;


                OnObjectGathered?.Invoke();
                ObjectPicked?.Invoke();
            }
        }
    }
   
    string CurrentObjectName()
    {
        string objectName = playerRaycaster.InteractedObjectName();
        return objectName;
    }
    #endregion

    void PrintAllRef()
    {
        //foreach (var objectInfo in pickAbleObject)
        //{
        //    Debug.Log("Object name :" + objectInfo.ObjectNameInSO);
        //    Debug.Log("Object ref :" + objectInfo.ObjectInPlayerHand);
        //}
    }

    
}


