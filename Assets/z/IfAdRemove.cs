using UnityEngine;

public class IfAdRemove : MonoBehaviour
{
    [SerializeField] string playerPrefsString = "noADS";
    [SerializeField] GameObject[] gameObjectsToHide;
    [SerializeField] GameObject[] gameObjectsToShow;
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt(playerPrefsString) == 1)
        {
            Show();
            Hide();
        }
    }

    void Show()
    {
        foreach (GameObject obj in gameObjectsToShow)
        {
            obj.SetActive(true);
        }
    }
    void Hide()
    {
        foreach (GameObject obj in gameObjectsToHide)
        {
            obj.SetActive(false);
        }
    }
}
