using UnityEngine;

public class SpecialItemInHand : MonoBehaviour
{
    public static SpecialItemInHand Instance;

    public GameObject punchItem;
    public GameObject gunItem;

    public bool handFreeAtMoment { get; private set; }  // Make it read-only outside

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        UpdateHandState(); // Ensure correct state at the start
    }

    public void UpdateHandState()
    {
        // If both items are inactive, hand is free
        handFreeAtMoment = !punchItem.activeSelf && !gunItem.activeSelf;
    }

    // Call this method whenever the active state of items changes
    public void SetItemState(GameObject item, bool state)
    {
        item.SetActive(state);
        UpdateHandState();  // Ensure hand state is updated
    }
}
