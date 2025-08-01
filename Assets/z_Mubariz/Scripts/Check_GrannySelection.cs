using UnityEngine;

public class Check_GrannySelection : MonoBehaviour
{
    [SerializeField] private GameObject grandpaGameObject;
    [SerializeField] private GameObject grannyGameObject;
    public static bool IsGrannyEnabled { get; private set; }

    private void OnEnable()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedGrannyIndex", 0);

        Debug.Log("Selected enemy idex is :" + selectedIndex);

        if (selectedIndex == 0 || selectedIndex == 3)
        {
            grandpaGameObject.SetActive(true);
            grannyGameObject.SetActive(false);
            IsGrannyEnabled = false;
        }
        else
        {
            grandpaGameObject.SetActive(false);
            grannyGameObject.SetActive(true);
            IsGrannyEnabled = true;
        }
    }
}
