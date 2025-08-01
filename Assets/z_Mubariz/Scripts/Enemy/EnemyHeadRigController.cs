using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HeadLookRigController : MonoBehaviour
{
    [Header("References")]
    public Transform enemy;   // The enemy or head bone root
    public Transform cat;     // The player/cat
    public Rig headRig;       // The Rig with Multi-Aim Constraint

    [Header("Settings")]
    public float maxLookDistance = 2f;       // How close the cat has to be
    public float blendDuration = 2f;         // Time to blend from 0 to 1 (or back)

    private float currentWeight = 0f;

    void Update()
    {
        float distance = Vector3.Distance(enemy.position, cat.position);

        // Calculate target weight based on distance
        float targetWeight = distance <= maxLookDistance ? 1f : 0f;

        // Gradually move toward target weight
        currentWeight = Mathf.MoveTowards(currentWeight, targetWeight, Time.deltaTime / blendDuration);

        // Apply to rig
        headRig.weight = currentWeight;
    }
}
