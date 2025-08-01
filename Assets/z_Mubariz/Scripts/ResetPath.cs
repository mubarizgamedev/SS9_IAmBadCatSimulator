using UnityEngine;
using UnityEngine.AI;

public class ResetPath : MonoBehaviour
{
    private NavMeshAgent agent;
    private float timer = 0f;
    private float resetInterval = 10f; // 10 seconds interval

    void Start()
    {
        // Grab the NavMeshAgent component from the same GameObject
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("No NavMeshAgent found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (agent == null) return; // Safety check

        timer += Time.deltaTime; // Count the seconds

        if (timer >= resetInterval)
        {
            agent.ResetPath(); // Reset the current path
            Debug.Log("Path reset at: " + Time.time + " seconds");
            timer = 0f; // Restart the timer
        }
    }
}
