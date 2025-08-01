using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyWandering : MonoBehaviour
{
    public static EnemyWandering Instance;

    private void Awake()
    {
        Instance = this;
    }

    bool canWander;
    public bool isWaiting = false;
    NavMeshAgent agent;
    List<Vector3> wayPoints;
    [SerializeField] Transform[] firstFloorWayPoints;
    [SerializeField] Transform[] secondFloorWayPoints;
    int currentWayPointIndex;
    [SerializeField] float distBeforeWaypoint = 0.5f;
    public Animator grannyAnimator;
    public RuntimeAnimatorController grannyWanderingAnimator;
    public RuntimeAnimatorController garnnyWatchingTvAnimator;
    public RuntimeAnimatorController grannyCookingAnimator;
    public float waitAtWayPoint;
    #region UNITY FUNCTIONS

    private void Start()
    {
        wayPoints = new List<Vector3>();
        AddFirstFloorWayPoints();
        Debug.Log("Entered granny wandering State");

        agent = GetComponentInParent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        Invoke(nameof(WanderTrue), 2f);
    }

    void WanderTrue()
    {
        canWander = true;
        grannyAnimator.runtimeAnimatorController = grannyWanderingAnimator;
    }

    public void Update()
    {
        if (canWander && !isWaiting)
        {
            if (!agent.pathPending && agent.remainingDistance <= distBeforeWaypoint)
            {
                //StartCoroutine(MoveToNextWaypointWithDelay());

                MoveToNextWaypoint();
            }
        }        
    }


    IEnumerator MoveToNextWaypointWithDelay()
    {
        grannyAnimator.runtimeAnimatorController = grannyCookingAnimator;
        isWaiting = true; 

        yield return new WaitForSeconds(waitAtWayPoint);
        grannyAnimator.runtimeAnimatorController = grannyWanderingAnimator;
        MoveToNextWaypoint();
        isWaiting = false;
    }

    #endregion


    #region HELPER FUNCTIONS

    void MoveToNextWaypoint()
    {
        if (wayPoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned to the granny");
            return;
        }

        Debug.Log("moving to next way point");

        // Pick a random waypoint
        currentWayPointIndex = Random.Range(0, wayPoints.Count);
        agent.SetDestination(wayPoints[currentWayPointIndex]);
    }
    #endregion

    public void AddFirstFloorWayPoints()
    {
        foreach (var item in firstFloorWayPoints)
        {
            wayPoints.Add(item.position);
        }
    }

    public void RemoveFirstFloorWayPoints()
    {
        foreach (var item in firstFloorWayPoints)
        {
            wayPoints.Remove(item.position);
        }
    }

    public void AddSecondFloorWayPoints()
    {
        foreach (var item in secondFloorWayPoints)
        {
            wayPoints.Add(item.position);
        }
    }

    public void RemoveSecondFloorWayPoints()
    {
        foreach (var item in secondFloorWayPoints)
        {
            wayPoints.Remove(item.position);
        }
    }

    

    public void UpdateWayPoints(List<Vector3> newWaypoints)
    {
        wayPoints.Clear();

        foreach (var item in newWaypoints)
        {
            wayPoints.Add(item);
        }
        Debug.Log("Granny waypoints updated");
    }
}
