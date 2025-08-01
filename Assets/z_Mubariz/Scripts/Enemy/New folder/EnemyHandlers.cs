using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Events;

public class EnemyHandlers : MonoBehaviour
{
    public static EnemyHandlers Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Header("Preferences")]
    [SerializeField] float maxTimeGrannyChaseCat = 15f;
    [SerializeField] float stopDistanceBeforeCat = 1.5f;
    [SerializeField] float detectionRadius = 5f;
    [SerializeField] float maxBodyConstraintToCat = 1f;


    [Header("Components")]
    [SerializeField] GameObject WanderingChildGameObject;
    [SerializeField] Transform catTransform;
    [SerializeField] AimConstraint headAimConstraint;
    [SerializeField] RotationConstraint bodyRotationConstraint;
    [SerializeField] Animator cameraAnimator;
    [SerializeField] GameObject fadeGObj;
    [SerializeField] GameObject catDeathAnimGameobject;
    [SerializeField] GameObject DogDeathAnimGameobject;
    [SerializeField] GameObject dangerMusicWhenChasingCat;
    [SerializeField] GameObject mainMusicGameObject;
    [SerializeField] GameObject bloodOverlayUI;

    [Header("Animation Parameter Strings")]
    [SerializeField] string animAttackBool = "isAttack";
    [SerializeField] string animAngerTrigger = "Anger";
    [SerializeField] string animReturnTrigger = "Return";
    [SerializeField] string cameraAnimShakeTrigger = "Shake";
    [SerializeField] string animAfterSpecial = "NowAnger";

    [Header("Events")]
    public UnityEvent OnGrannyHitCat;
    public UnityEvent OnAttackStart;
    public static event Action OnGrannyNear;

    private Animator m_Animator;
    public NavMeshAgent m_Agent;
    private bool isChasingCat;
    private float chaseTimer;
    public static bool canAttackCat;

    int selectedIndexForCat;
    int selectedIndexForGranny;

    #region UNITY FUNCTIONS




    private void OnEnable()
    {
        Debug.Log(IsGrannySelected());
    }

    private void Start()
    {
        selectedIndexForCat = PlayerPrefs.GetInt("SelectedCatIndex", 0);
        selectedIndexForGranny = PlayerPrefs.GetInt("SelectedGrannyIndex", 0);
        m_Animator = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();


        if (WanderingChildGameObject != null)
        {
            Debug.Log("wandering gameobject is preset");
        }
        else
        {
            Debug.LogWarning("Wandering gameobject is not present");
        }

        PickableObject.OnObjectHitGranny += PickableObject_OnObjectHitGranny;
        PunchingGlove.OnPunchGranny += PunchedGranny;
        CurrentBullet.OnCurrentBulletHitGranny += CurrentBullet_OnCurrentBulletHitGranny;
        FireBullet.OnFireBulletHitGranny += FireBullet_OnFireBulletHitGranny;
        BeeBullet.OnBeeBulletHitGranny += BeeBullet_OnBeeBulletHitGranny;


    }
    private void OnDestroy()
    {
        PickableObject.OnObjectHitGranny -= PickableObject_OnObjectHitGranny;
        PunchingGlove.OnPunchGranny -= PunchedGranny;
        CurrentBullet.OnCurrentBulletHitGranny -= CurrentBullet_OnCurrentBulletHitGranny;
        FireBullet.OnFireBulletHitGranny -= FireBullet_OnFireBulletHitGranny;
        BeeBullet.OnBeeBulletHitGranny -= BeeBullet_OnBeeBulletHitGranny;
    }

    private void Update()
    {
        if (isChasingCat)
        {
            if (dangerMusicWhenChasingCat.activeSelf == false)
            {
                dangerMusicWhenChasingCat.SetActive(true);
                mainMusicGameObject.SetActive(false);
            }
            ChaseCat();
        }

        // Check if Granny is within 5 units of the cat
        if (Vector3.Distance(transform.position, catTransform.position) <= detectionRadius)
        {
            OnGrannyNear?.Invoke();  // Invoke the event if Granny is near the cat
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Grandpa or granny index is : " + selectedIndexForGranny);
            Debug.Log(IsGrannySelected());
        }
    }




    #endregion

    #region HELPER FUNCTIONS

    private void PickableObject_OnObjectHitGranny()
    {
        StartChasingCat();
        if (IsGrannySelected())
        {
            SFX_Manager.PlaySound(SFX_Manager.Instance.grannyOhNo);
        }
        else
        {
            SFX_Manager.PlayRandomSound(SFX_Manager.Instance.angryTalkGrandpa);
        }


    }

    public void PunchedGranny()
    {
        m_Animator.SetTrigger("Punch");
    }

    private void CurrentBullet_OnCurrentBulletHitGranny()
    {
        m_Animator.SetTrigger("Current");
        m_Agent.isStopped = true;
    }

    private void FireBullet_OnFireBulletHitGranny()
    {
        m_Animator.SetTrigger("Fire");
        m_Agent.isStopped = true;
    }

    private void BeeBullet_OnBeeBulletHitGranny()
    {
        m_Animator.SetTrigger("Bee");
        m_Agent.isStopped = true;
    }

    public void AngerAfterSpecialAttack()
    {
        m_Animator.SetTrigger(animAfterSpecial);
        StartChasingCat();
    }

    private void StartChasingCat()
    {
        //SOUNDS WHILE CHASING START
        if (IsGrannySelected())
        {
            SFX_Manager.PlaySound(SFX_Manager.Instance.angryTalkGranny);
        }
        else
        {
            SFX_Manager.PlayRandomSound(SFX_Manager.Instance.angryTalkGrandpa);
        }

        Debug.Log("start chasing cat");

        SFX_Manager.PlaySound(SFX_Manager.Instance.OnDangerSounds, 0.5f);
        isChasingCat = true;
        m_Agent.isStopped = false;
        chaseTimer = 0f;
        if (m_Animator != null)
        {
            m_Animator.SetTrigger(animAngerTrigger);
        }
        else
        {
            Debug.Log("Animator is missing");
        }
        if (WanderingChildGameObject != null)
        {
            WanderingChildGameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Wandering gameobject is not present");
        }
    }



    private void ChaseCat()
    {
        if (!isChasingCat) return;

        m_Agent.SetDestination(catTransform.position);


        chaseTimer += Time.deltaTime;

        float distanceToCat = Vector3.Distance(transform.position, catTransform.position);

        if (distanceToCat <= stopDistanceBeforeCat)
        {
            canAttackCat = true;
            AttackCat();
        }
        else if (chaseTimer >= maxTimeGrannyChaseCat)
        {
            ResetState();
        }
    }


    private void AttackCat()
    {
        if (selectedIndexForGranny == 1 || selectedIndexForGranny == 3)  // this is for granny
        {
            SFX_Manager.PlaySound(SFX_Manager.Instance.GrannyAngerNewspaper, 1f);
        }
        else   // this grandpa
        {
            SFX_Manager.PlayRandomSound(SFX_Manager.Instance.angryTalkGrandpa);

        }
        bloodOverlayUI.SetActive(true);
        dangerMusicWhenChasingCat.SetActive(false);
        mainMusicGameObject.SetActive(true);
        isChasingCat = false;
        m_Agent.isStopped = true;
        OnAttackStart?.Invoke();
        bodyRotationConstraint.constraintActive = true;
        bodyRotationConstraint.weight = maxBodyConstraintToCat;
        //headAimConstraint.weight = maxHeadConstraintToCat;


        if (canAttackCat)
        {
            if (IsCatInDetectionRadius())
            {
                m_Animator.SetBool(animAttackBool, true);
                if (AdmobAdsManager.Instance)
                {
                    if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
                    {
                      //  Firebase.Analytics.FirebaseAnalytics.LogEvent("LevelFail");
                    }
                }

            }
        }

    }

    private bool IsCatInDetectionRadius()
    {
        return Vector3.Distance(transform.position, catTransform.position) <= detectionRadius;
    }



    public void ResetState()
    {
        Debug.Log("reseting state");
        chaseTimer = maxTimeGrannyChaseCat;
        dangerMusicWhenChasingCat.SetActive(false);
        mainMusicGameObject.SetActive(true);
        isChasingCat = false;
        bodyRotationConstraint.constraintActive = false;
        bodyRotationConstraint.weight = 0;
        WanderingChildGameObject.SetActive(true);
        m_Agent.ResetPath();
        m_Agent.isStopped = false;     
        Debug.Log("attack bool paremeter is" + m_Animator.GetBool(animAttackBool));
        m_Animator.SetBool("Attack", false);
        m_Animator.SetBool("Wander", true);
    }


    #endregion

    #region ANIMATION EVENTS

    public void ReturnToWalkingAnimation()
    {
        m_Animator.SetTrigger(animReturnTrigger);
    }

    public void GrannyAttacked()
    {
        OnGrannyHitCat?.Invoke();
        if (IsCatSelected())
        {
            SFX_Manager.PlaySound(SFX_Manager.Instance.catCrySound);
        }
        else
        {
            SFX_Manager.PlaySound(SFX_Manager.Instance.dogCrySound);
        }
        SFX_Manager.PlaySound(SFX_Manager.Instance.catHitSound);
        SFX_Manager.PlayRandomSound(SFX_Manager.Instance.OnDieSounds, 0.5f);
        cameraAnimator.SetTrigger(cameraAnimShakeTrigger);
    }

    public void EnableCatDeath()
    {
        if (!IsCatSelected())
        {
            catDeathAnimGameobject.SetActive(false);
            DogDeathAnimGameobject.SetActive(true);
        }
        else
        {
            DogDeathAnimGameobject.SetActive(false);
            catDeathAnimGameobject.SetActive(true);
        }

    }

    private bool IsCatSelected()
    {
        if (selectedIndexForCat == 0 || selectedIndexForCat == 4)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool IsGrannySelected()
    {
        if (selectedIndexForGranny == 1 || selectedIndexForGranny == 2)
        {
            Debug.Log("Granny is selected");
            return true;
        }
        else
        {
            Debug.Log("Grandpa is selected");
            return false;
        }
    }

    public void PunchSound()
    {
        SFX_Manager.PlaySound(SFX_Manager.Instance.punchSound);
        SFX_Manager.PlaySound(SFX_Manager.Instance.grannyOhNo);

    }

    #endregion  
}
