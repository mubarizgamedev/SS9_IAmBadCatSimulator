using UnityEngine;

public class CatDeathSelection : MonoBehaviour
{
    [Header("Assign 3 Cat GameObjects")]
    public GameObject[] catVariants; // Assign 3 variants in Inspector

    private void Start()
    {
        ActivateSelectedCat();
    }
    private void OnEnable()
    {
        ActivateSelectedCat();
    }
    void ActivateSelectedCat()
    {
        if (catVariants == null || catVariants.Length < 3)
        {
            Debug.LogError("Please assign exactly 3 GameObjects in the Inspector.");
            return;
        }

        // Get selected index from PlayerPrefs (default to 0 if not set)
        int selectedIndex = PlayerPrefs.GetInt("SelectedCatIndex", 0);
        selectedIndex = Mathf.Clamp(selectedIndex, 0, catVariants.Length - 1);

        // Disable all cat variants
        foreach (GameObject cat in catVariants)
        {
            if (cat != null)
                cat.SetActive(false);
        }

        // Enable only the selected one
        if (catVariants[selectedIndex] != null)
            catVariants[selectedIndex].SetActive(true);
    }
}
