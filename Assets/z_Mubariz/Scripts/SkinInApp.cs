using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinInApp : MonoBehaviour
{
    int selectedIndexForCat;
    [SerializeField] GameObject catSkin;
    [SerializeField] GameObject dogSkin;
    private void Start()
    {
        selectedIndexForCat = PlayerPrefs.GetInt("SelectedCatIndex", 0);
        if (IsCatSelected())
        {
            catSkin.SetActive(true);
            dogSkin.SetActive(false);
        }
        else
        {
            dogSkin.SetActive(true);
            catSkin.SetActive(false);
        }
    }

    private bool IsCatSelected()
    {
        if (selectedIndexForCat == 0 || selectedIndexForCat == 4)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
