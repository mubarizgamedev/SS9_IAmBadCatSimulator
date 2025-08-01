using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHandler : MonoBehaviour
{
    public static EnemyHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Header("Preferences")]
    [SerializeField] float maxTimeGrannyChaseCat = 15f;
    [SerializeField] float stopDistanceBeforeCat = 1.5f;
    [SerializeField] float detectionRadius = 5f;
    [SerializeField] float maxBodyConstraintToCat = 1f;
    [SerializeField] private float soundInterval = 5f;

    [Header("Components")]
    [SerializeField] Objective_Manager objectiveManager;
    [SerializeField] NewObjectiveManager newobjecttiveManager;
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
    [SerializeField] GameObject bat2;
    [SerializeField] GameObject bat1;
    [SerializeField] RuntimeAnimatorController oldController;

    [Header("Animation Parameter Strings")]
    [SerializeField] string animAttackBool = "isAttack";
    [SerializeField] string animAngerTrigger = "Anger";
    [SerializeField] string animReturnTrigger = "Return";
    [SerializeField] string animAfterSpecial = "NowAnger";
    [SerializeField] string cameraAnimShakeTrigger = "Shake";

    [Header("Events")]

    public UnityEvent OnAttackStart;
    public static event Action OnGrannyNear;

    private Animator m_Animator;
    public NavMeshAgent m_Agent;
    public bool isChasingCat;
    private float chaseTimer;
    public static bool canAttackCat;

    int selectedIndexForCat;
    int selectedIndexForGranny;

    int roundOffIntForChaseTime ;
    public static event Action OnGrannyHitCat;
    [Space(10)]
    [SerializeField] Text safeText;

    #region UNITY FUNCTIONS

    private void OnEnable()
    {
        selectedIndexForCat = PlayerPrefs.GetInt("SelectedCatIndex", 0);
        selectedIndexForGranny = PlayerPrefs.GetInt("SelectedGrannyIndex", 0);
    }

    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();

        FirstObjective.OnChangeChannel += TriggerAngerGranny;
        PickableObject.OnObjectHitGranny += TriggerAngerGranny;
        PunchingGlove.OnPunchGranny += PunchedGranny;
        CurrentBullet.OnCurrentBulletHitGranny += CurrentBullet_OnCurrentBulletHitGranny;
        FireBullet.OnFireBulletHitGranny += FireBullet_OnFireBulletHitGranny;
        BeeBullet.OnBeeBulletHitGranny += BeeBullet_OnBeeBulletHitGranny;

        

        // AJ
        m_Animator = this.gameObject.GetComponent<Animator>();
        m_Agent = this.gameObject.GetComponent<NavMeshAgent>();


        if (WanderingChildGameObject != null)
        {
            Debug.Log("wandering gameobject is preset");
        }
        else
        {
            Debug.LogWarning("Wandering gameobject is not present");
        }

    }

    

    private void OnDestroy()
    {
        FirstObjective.OnChangeChannel -= TriggerAngerGranny;
        PickableObject.OnObjectHitGranny -= TriggerAngerGranny;
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
    }

    #endregion

    #region HELPER FUNCTIONS

    public void TriggerAngerGranny()
    {
        Debug.Log("Granny is angry");
        m_Animator.runtimeAnimatorController = oldController;
        StartChasingCat();
    }

    public void PunchedGranny()
    {
        m_Animator.runtimeAnimatorController = oldController;
        m_Animator.SetTrigger("Punch");
    }

    private void CurrentBullet_OnCurrentBulletHitGranny()
    {
        m_Animator.runtimeAnimatorController = oldController;
        m_Animator.SetTrigger("Current");
        m_Agent.isStopped = true;
    }

    private void FireBullet_OnFireBulletHitGranny()
    {
        m_Animator.runtimeAnimatorController = oldController;
        m_Animator.SetTrigger("Fire");
        m_Agent.isStopped = true;
    }

    private void BeeBullet_OnBeeBulletHitGranny()
    {
        m_Animator.runtimeAnimatorController = oldController;
        m_Animator.SetTrigger("Bee");
        m_Agent.isStopped = true;
    }
    public void Btn_call()
    {
        //StartChasingCat();
        TriggerAngerGranny();
    }

    public void StartChasingCat()
    {
        bat1?.SetActive(true);
        bat2?.SetActive(true);
        m_Animator.ResetTrigger(animAfterSpecial);
        m_Animator.SetBool("Wander", false);
        SFX_Manager.PlaySound(SFX_Manager.Instance.angryTalkGranny);
        Debug.Log("sound times" );
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
            WanderingChildGameObject.SetActive(true);
        }
    }


    private void AttackCat()
    {
        NewObjectiveManager.Instance.GranniesBats(true);
        SFX_Manager.PlaySound(SFX_Manager.Instance.GrannyAngerNewspaper, 1f);

        bloodOverlayUI.SetActive(true);
       
       
        dangerMusicWhenChasingCat.SetActive(false);
        mainMusicGameObject.SetActive(true);
        isChasingCat = false;
        m_Agent.isStopped = true;
        OnAttackStart?.Invoke();
        bodyRotationConstraint.constraintActive = true;
        bodyRotationConstraint.weight = maxBodyConstraintToCat;


        if (canAttackCat)
        {
            if (IsCatInDetectionRadius())
            {
                m_Animator.SetBool(animAttackBool, true);
                if (AdmobAdsManager.Instance)
                {
                    if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
                    {
                        Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_" + objectiveManager.CurrentLevel() + " Fail" );
                        Debug.Log("Level " + objectiveManager.CurrentLevel() + " Fail");
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
        Debug.Log("Reset state activated");
        dangerMusicWhenChasingCat.SetActive(false);
        mainMusicGameObject.SetActive(true);
        chaseTimer = maxTimeGrannyChaseCat;
        isChasingCat = false;
        canAttackCat = false;
        bodyRotationConstraint.constraintActive = false;
        bodyRotationConstraint.weight = 0;
        m_Agent.ResetPath();
        m_Agent.isStopped = false;
        m_Animator.SetBool("Attack", false);
        m_Animator.SetBool("Wander", true);
    }


    #endregion

    #region ANIMATION EVENTS

    public void ReturnToWalkingAnimation()
    {
        m_Animator.SetTrigger(animReturnTrigger);
    }

    public void AngerAfterSpecialAttack()
    {
        m_Animator.SetTrigger(animAfterSpecial);
        StartChasingCat();
    }

    public void GrannyAttacked()
    {
        Debug.Log("Granny Attacked function in Enemy Handler has been called by the animation event");

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

    private bool IsCatSelected()
    {
        if (selectedIndexForCat == 0 || selectedIndexForCat == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsGrannySelected()
    {
            return true;
    }


    public void EnableCatDeath()
    {
       
            catDeathAnimGameobject.SetActive(true);
    }

    public void PunchSound()
    {
        SFX_Manager.PlaySound(SFX_Manager.Instance.punchSound);
        SFX_Manager.PlaySound(SFX_Manager.Instance.grannyOhNo);

    }

    
    #endregion  

    public void SetRotationConstrainToZero()
    {
        bodyRotationConstraint.constraintActive = false;
        bodyRotationConstraint.weight = 0;
    }
}
