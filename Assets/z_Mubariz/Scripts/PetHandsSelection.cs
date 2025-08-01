using UnityEngine;

public class PetHandsSelection : MonoBehaviour
{
    public Material[] catHandMaterials;

    private int indexOfPet;

    SkinnedMeshRenderer skinnedMeshRenderer;

    private void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        
    }

    private void OnEnable()
    {
        indexOfPet = PlayerPrefs.GetInt("SelectedCatIndex", 0);

        AssignMaterial(indexOfPet);
    }

    void AssignMaterial(int index)
    {
        if (skinnedMeshRenderer != null && catHandMaterials.Length > index)
        {
            skinnedMeshRenderer.material = catHandMaterials[index];
            Debug.Log("Assigned material for cat hands at index: " + index);
        }
        else
        {
            Debug.LogWarning("Material assignment failed! Check SkinnedMeshRenderer or material array size.");
        }
    }


}
