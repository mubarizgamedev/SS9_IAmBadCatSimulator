using UnityEngine;

public class GrannySkinSelection : MonoBehaviour
{
    public Material pinkMaterial;
    public Material blueMaterial;
    public Material yellowMaterial;
    public Material greenMaterial;

    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    Material[] materials;

    private void OnEnable()
    {
        Invoke(nameof(ChangeSkin), 0.2f);
    }

    private void ChangeSkin()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedGrannyIndex", 0);

        Debug.Log("Selected index for  granny skin : "+ selectedIndex);

         materials = skinnedMeshRenderer.materials;

        if (selectedIndex == 0)
        {
            materials[1] = pinkMaterial;
        }
        if (selectedIndex == 1)
        {
            materials[1] = blueMaterial;
        }
        if (selectedIndex == 2)
        {
            materials[1] = yellowMaterial;
        }
        else if(selectedIndex == 3)
        {
            materials[1] = greenMaterial;
        }
        skinnedMeshRenderer.materials = materials;
    }
}
