using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandpaSkinSelection : MonoBehaviour
{
    public Material purpleClothMaterial;
    public Material redClothMaterial;

    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    Material[] materials;

    private void OnEnable()
    {
        Invoke(nameof(ChangeSkin), 0.2f);
    }

    private void ChangeSkin()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedGrannyIndex", 0);

        Debug.Log("Selected index for  granny skin : " + selectedIndex);

        materials = skinnedMeshRenderer.materials;

        if (selectedIndex == 0)
        {
            materials[0] = purpleClothMaterial;
        }
        else if (selectedIndex == 3)
        {
            materials[0] = redClothMaterial;            
        }
        skinnedMeshRenderer.materials = materials;
    }
}
