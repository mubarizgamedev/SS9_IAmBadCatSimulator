using UnityEngine;

public class InappMainmenu : MonoBehaviour
{
    [SerializeField] GameObject crossButton;
    [SerializeField] GameObject crossButton2;
    [SerializeField] float timeAfterCrossShow = 3f;
    public GameObject secondMusicGameobject;
    private void Start()
    {
        Invoke(nameof(ShowCrossButton), timeAfterCrossShow);
    }
    private void ShowCrossButton()
    {
        crossButton.SetActive(true);
        crossButton2.SetActive(true);
    }

    private void OnDisable()
    {
        FindObjectOfType<FirstMusic>()?.DestroySelf();
        secondMusicGameobject.SetActive(true);

    }
}
