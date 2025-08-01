using UnityEngine;

public class CatMaterialSelection : MonoBehaviour
{
    public Material firstMaterial;
    public Material secondMaterial;
    public Material thirdMaterial;

    public SkinnedMeshRenderer skinnedMeshRenderer;

    int selectedCatIndex;

    void Start()
    {
        selectedCatIndex = PlayerPrefs.GetInt("SelectedCatIndex", 0);
        SelectMaterial();
    }

    void SelectMaterial()
    {
        Material[] materials = skinnedMeshRenderer.materials;

        switch (selectedCatIndex)
        {
            case 0:
                materials[0] = firstMaterial;
                break;
            case 1:
                materials[0] = secondMaterial;
                break;
            case 2:
                materials[0] = thirdMaterial;
                break;
            default:
                Debug.LogWarning("Invalid cat index selected: " + selectedCatIndex);
                break;
        }

        skinnedMeshRenderer.materials = materials;
    }
}
