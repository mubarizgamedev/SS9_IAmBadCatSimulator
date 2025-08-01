using UnityEngine;

public class DeadCatSkinSelection : MonoBehaviour
{
    public Material brownCatMaterial;
    public Material blackCatMaterial;
    public Material redCatMaterial;
    public Material greenCatMaterial;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;



    private void OnEnable()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedCatIndex", 0);

        Debug.Log("Selected enemy idex is :" + selectedIndex);

        if (selectedIndex == 0)
        {
            skinnedMeshRenderer.material = brownCatMaterial;
        }
        else if (selectedIndex == 1)
        {
            skinnedMeshRenderer.material = blackCatMaterial;
        }
        else if (selectedIndex == 2)
        {
            skinnedMeshRenderer.material = redCatMaterial;
        }
        else if (selectedIndex == 3)
        {
            skinnedMeshRenderer.material = greenCatMaterial;
        }
        
    }
}
