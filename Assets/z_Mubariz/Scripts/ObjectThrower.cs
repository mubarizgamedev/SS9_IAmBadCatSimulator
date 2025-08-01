using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ObjectThrower : MonoBehaviour
{
    [SerializeField] float waitToThrow;
    [SerializeField] GameObject objectToDisable;
    public GameObject currentThrowableObject;

    [SerializeField] float throwForce = 10f;
    [SerializeField] Transform throwPosition;
    [SerializeField] Transform targetPosition;
    [SerializeField] AudioClip throwSound;
    [SerializeField] GameObject throwUI;

    public bool canPlaySound;

    /////////////////////////////////// 
    /// 
    ///    EVENTS
    /// 
    ///////////////////////////////////

    public UnityEvent OnObjectThrownEvent;
    public static event Action OnObjectThrown;
    public static event Action CatThrowObject;


    [SerializeField]
    GameObject[] allThrowAbleObjects;

    bool canThrow;
    Rigidbody m_Rb;


    public static ObjectThrower Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #region HELPER FUNCTIONS



    public void Throwobject()
    {
        ThrowFunctionality();

    }

    void ThrowFunctionality()
    {
        
        FindCurrentObject_String();
        if (currentThrowableObject != null)
        {
            Invoke(nameof(NowThrow), 0.5f);
            
        }

    }

    public void ResetState()
    {
        foreach (var gObj in allThrowAbleObjects)
        {
            gObj.SetActive(false);
        }
        throwUI.SetActive(false);
        ObjectPicker.canGrabObject = true;        
    }

    

    void FindCurrentObject_String()
    {
        foreach (var gObj in allThrowAbleObjects)
        {
            if (gObj.activeSelf)
            {
                Debug.Log(gObj.name + " is enabled in hand");
                currentThrowableObject = gObj;

                objectToDisable = gObj;
                break;
            }
        }
    }


    
    void NowThrow()
    {
        GameObject cloneObject = Instantiate(currentThrowableObject, throwPosition.position, Quaternion.identity);
        //Debug.LogError("....");
        if (cloneObject.tag == "Dart")
        {
            cloneObject.transform.rotation = Quaternion.LookRotation(throwPosition.forward) * Quaternion.Euler(75, 0, 0);
        }

        if (cloneObject != null)
        {
            Collider col = cloneObject.GetComponent<Collider>();
            if (col != null)
            {
                col.isTrigger = false;
            }
            else
            {
                Debug.LogWarning("can't find collider of the gameobject");
            }
            m_Rb = null;
            Rigidbody rb = cloneObject.GetComponent<Rigidbody>();
            if (rb != null)
            {

                CatThrowObject?.Invoke();

                rb.useGravity = false;
                rb.isKinematic = false;

                cloneObject.layer = 8;

                canThrow = false;
                m_Rb = rb;
                //Debug.Log("Delaying for seconds: " + waitToThrow);
                Invoke(nameof(NowThrow), 0.35f);


                rb.AddForce(throwPosition.forward * throwForce, ForceMode.VelocityChange);



                if (throwSound != null)
                {
                    SFX_Manager.PlaySound(throwSound);
                }

                currentThrowableObject = null;

                if (objectToDisable != null)
                {
                    objectToDisable.SetActive(false);
                }

                //OBJECT THROWN 

                ObjectPicker.canGrabObject = true;

                OnObjectThrownEvent?.Invoke();
                StartCoroutine(SoundTimer());

            }
            else
            {
                Debug.Log("rigidbody not found");
            }

        }
    }

    IEnumerator SoundTimer()
    {
        canPlaySound = true;
        yield return new WaitForSeconds(2f);
        canPlaySound = false;
    }

    #endregion
}
