using UnityEngine;

public class CatHandsMatSelector : MonoBehaviour
{
    [Header("Assign 3 Materials")]
    public Material[] catMaterials; // Make sure size is 3 in Inspector

    private void Start()
    {
        ApplySelectedMaterial();
    }
    private void OnEnable()
    {
        ApplySelectedMaterial();
    }
    void ApplySelectedMaterial()
    {
        // Safety checks
        if (catMaterials == null || catMaterials.Length < 3)
        {
            Debug.LogError("Please assign exactly 3 materials in the Inspector.");
            return;
        }

        if (!PlayerPrefs.HasKey("SelectedCatIndex"))
        {
            Debug.LogWarning("selectedCatIndex not found in PlayerPrefs. Using default index 0.");
        }

        int index = PlayerPrefs.GetInt("SelectedCatIndex", 0); // Default to 0 if not set
        index = Mathf.Clamp(index, 0, catMaterials.Length - 1); // Clamp to valid range

        SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();

        if (smr == null)
        {
            Debug.LogError("SkinnedMeshRenderer not found on this GameObject.");
            return;
        }

        // Copy current materials
        Material[] currentMaterials = smr.materials;

        // Replace only the first material
        currentMaterials[0] = catMaterials[index];

        // Apply back
        smr.materials = currentMaterials;
    }
}
