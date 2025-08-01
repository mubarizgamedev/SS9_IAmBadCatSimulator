using UnityEngine;

public class SoundSelectionForGranReaction : MonoBehaviour
{
    int selectedIndexForGranny;
    [SerializeField] GameObject gObjWithGrandpaSound;
    [SerializeField] GameObject gObjWithGrannySound;

    private void Start()
    {
        selectedIndexForGranny = PlayerPrefs.GetInt("SelectedGrannyIndex", 0);
    }

    public bool IsGrannySelected()
    {
        if (selectedIndexForGranny == 1 || selectedIndexForGranny == 2)
        {
            Debug.Log("Granny is selected");
            return true;
        }
        else
        {
            Debug.Log("Grandpa is selected");
            return false;
        }
    }
    private void OnEnable()
    {
        if (IsGrannySelected())
        {
            gObjWithGrannySound.SetActive(true);
            gObjWithGrandpaSound.SetActive(false);
        }
        else
        {
            gObjWithGrannySound.SetActive(false);
            gObjWithGrandpaSound.SetActive(true);
        }
    }

    private void OnDisable()
    {
        gObjWithGrannySound.SetActive(false);
        gObjWithGrandpaSound.SetActive(false);
    }
}
