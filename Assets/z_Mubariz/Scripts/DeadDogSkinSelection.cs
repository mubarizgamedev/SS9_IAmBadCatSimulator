using UnityEngine;

public class DeadDogSkinSelection : MonoBehaviour
{
    public Material orangeDogMaterial;
    public Material blueDogMaterial;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;



    private void OnEnable()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedCatIndex", 0);

        Debug.Log("Selected enemy idex is :" + selectedIndex);

        if (selectedIndex == 1)
        {
            skinnedMeshRenderer.material = orangeDogMaterial;
        }
        else if (selectedIndex == 4)
        {
            skinnedMeshRenderer.material = blueDogMaterial;
        }

    }
}
