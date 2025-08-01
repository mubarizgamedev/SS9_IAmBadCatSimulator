using UnityEngine;

public class GrannySelector : MonoBehaviour
{
    public Animator grannyAnimator;
    [Space(5)]
    public Avatar firstAvator;
    public Avatar secondAvator;
    public Avatar thirdAvatar;
    [Space(5)]
    public GameObject firstGranny;
    public GameObject secondGranny;
    public GameObject thirdGranny;

    [Space(5)]
    public Material blueShirtMaterial;
    public Material purpleShirtMaterial;



    int selectedGrannyIndex;


    private void Start()
    {
        selectedGrannyIndex = PlayerPrefs.GetInt("SelectedGrannyIndex");
        SelectGranny();
    }

    private void OnEnable()
    {
        SelectGranny();
    }

    void SelectGranny()
    {
        Debug.Log("Selected Granny Index is : " + selectedGrannyIndex);
        if (selectedGrannyIndex == 0)
        {
            firstGranny.SetActive(true);
            grannyAnimator.avatar = firstAvator;
            secondGranny.SetActive(false);
        }
        else if (selectedGrannyIndex == 1 || selectedGrannyIndex == 2)
        {
            secondGranny.SetActive(true);
            grannyAnimator.avatar = secondAvator;
            firstGranny.SetActive(false);
            //SetShirt();
        }
    }
    //void SetShirt()
    //{
    //    Material[] mats = secondGrannRendrer.materials;

    //    if (selectedGrannyIndex == 1)
    //    {
    //        // Set blue shirt
    //        mats[1] = blueShirtMaterial;
    //    }
    //    else if (selectedGrannyIndex == 2)
    //    {
    //        // Set purple shirt
    //        mats[1] = purpleShirtMaterial;
    //    }

    //    secondGrannRendrer.materials = mats;
    //}

}
