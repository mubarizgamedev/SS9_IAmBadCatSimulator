using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField] GameObject guideGameobject;
    [SerializeField] GameObject firstGuide;
    [SerializeField] GameObject gamePlayPanel;
    [SerializeField] float timeAfterDisable;
    
    public void OnClickOnNext()
    {
        Invoke(nameof(DisableGameobject), timeAfterDisable);
    }

    void DisableGameobject()
    {
        firstGuide.SetActive(false);
        guideGameobject.SetActive(true);
        gamePlayPanel.SetActive(true);
    }


}
