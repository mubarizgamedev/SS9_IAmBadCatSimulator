using UnityEngine;

public class Restarting : MonoBehaviour
{

    [SerializeField] Objective_Manager objective_Manager;
    [SerializeField] Transform player;
    [SerializeField] Transform playerPositionAtStart;
    public void RestartLatestObjective()
    {
        objective_Manager.NextObjective();
        EnemyHandler.Instance.ResetState();
        player.position = playerPositionAtStart.position;
        player.rotation = playerPositionAtStart.rotation;
    }
}
