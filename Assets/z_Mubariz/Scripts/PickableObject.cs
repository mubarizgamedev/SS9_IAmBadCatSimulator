using System;
using System.Collections;
using UnityEngine;


public class PickableObject : MonoBehaviour
{

    bool canMove;
    float elapsedTime;
    bool canDamageGranny = true;
    float durationToReachPlayer = 0.25f;
    Rigidbody rb;
    float catGrabTime = 0.3f;

    /////////////////////////////////// 
    /// 
    ///     LERPING POSITION
    /// 
    ///////////////////////////////////
    Vector3 startPosition;
    Transform target;

    [Tooltip("This text should be same as the text given in object picker script")]
    [SerializeField] string stringOfObject;


    /////////////////////////////////// 
    /// 
    ///   EVENTS
    /// 
    ///////////////////////////////////

    public static event Action OnObjectReachedPlayer;
    public static event Action OnObjectHitGranny;
    public static event Action OnGuideObjectHitGranny;
    public static event Action CatGrabObject;
    public static event Action KitcenObjectBroken;
    Collider M_Collider;
    bool kitchenObjBreakCall = true;

    #region UNITY FUNCTIONS  

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        M_Collider = GetComponent<Collider>();
        target = FindFirstObjectByType<ObjectTargetAtPlayer>().transform;
    }

    private void Update()
    {
        MoveObject();
    }


    #endregion


    #region HELPER FUNCTIONS

    void MoveObject()
    {
        if (canMove)
        {
            if (elapsedTime < durationToReachPlayer)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / durationToReachPlayer; // Normalize to 0 - 1 range
                if (elapsedTime >= catGrabTime)
                {
                    //Debug.Log("Cat Grab Object by hand");
                    CatGrabObject?.Invoke();
                }
                transform.position = Vector3.Lerp(startPosition, target.position, t);
            }
            else
            {
                transform.position = target.position; // Ensure exact final position
                canMove = false; // Stop movement only when finished
                OnObjectReachedPlayer?.Invoke();
            }
        }
    }

    public void MoveObject_TowardPlayer()
    {
        startPosition = transform.position;
        elapsedTime = 0f;
        canMove = true;
    }

    public string GetObjectName()
    {
        return stringOfObject;
    }
    #endregion



    private void OnCollisionEnter(Collision collision)
    {

        if (ObjectThrower.Instance.canPlaySound)
        {
            SFX_Manager.PlayRandomSound(SFX_Manager.Instance.hitObjectRandom, 1f);
        }

        //Debug.Log("collided with " + collision.gameObject.name);
        if (gameObject.CompareTag("Kitchen"))
        {
            if (kitchenObjBreakCall)
            {
                KitcenObjectBroken?.Invoke();
                kitchenObjBreakCall = false;
            }
            
        }

        rb.useGravity = true;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (canDamageGranny)
            {
                {

                    Debug.Log("Granny Hit");
                    OnGuideObjectHitGranny?.Invoke();
                    AngerGranny();


                    M_Collider.isTrigger = false;
                    canDamageGranny = false;
                }
            }
        }

        if (M_Collider.gameObject.activeSelf)
        {
            StartCoroutine(TriggerTrue(M_Collider, rb));
        }
    }
    IEnumerator TriggerTrue(Collider col,Rigidbody rb)
    {
        yield return new WaitForSeconds(2f);
        col.isTrigger = true;
        rb.isKinematic = true;
    }

    public void AngerGranny()
    {
        if (EnemyHandler.Instance)
            EnemyHandler.Instance.Btn_call();
        Debug.Log("...............................");
    }

    public void PushObject(float time)
    {
        StartCoroutine(PushCoroutine(time));
    }

    IEnumerator PushCoroutine(float t)
    {
        yield return new WaitForSeconds(t);

        M_Collider.isTrigger = false;
        rb.isKinematic = false;

        // ✅ Use CameraFreezZ.Instance for direction
        Vector3 camForward = CameraFreezZ.Instance.transform.forward;
        Vector3 camRight = CameraFreezZ.Instance.transform.right;

        Vector3 pushDir = (camForward + (-camRight) * 0.5f).normalized;
        float forceAmount = 5f;

        rb.AddForce(pushDir * forceAmount, ForceMode.Impulse);

        Debug.Log("Object has been pushed");
    }
}

