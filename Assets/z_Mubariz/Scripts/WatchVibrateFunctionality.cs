using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WatchVibrateFunctionality : MonoBehaviour
{
    public GameObject firstConvoPanel;
    public float timeAfterPanelActive;
    public GameObject handImage;
    bool canPanelDeactivate;
    bool startTimer;

    private void Start()
    {
        handImage.SetActive(false);
        TouchCheck.OnTouch += OnFirstConvoPanelBackClicked;
        if(PlayerPrefs.GetInt("Tutorial", 0) == 0  )
        {
            startTimer = true;
        }

    }

    private void OnDestroy()
    {
        TouchCheck.OnTouch -= OnFirstConvoPanelBackClicked;
    }

    private void Update()
    {
        if (startTimer)
        {
            timeAfterPanelActive += Time.deltaTime;
            if (timeAfterPanelActive >= 5f)
            {
                canPanelDeactivate = true;
                handImage.SetActive(true);
            }
        }
    }
    void OnFirstConvoPanelBackClicked()
    {
        

        if (!canPanelDeactivate)
            return;

        if (canPanelDeactivate)
        {
            StartCoroutine(RemovePanel());
        }
    }

    IEnumerator RemovePanel()
    {
        yield return new WaitForSeconds(2f);
        firstConvoPanel.SetActive(false);
        PlayerPrefs.SetInt("Tutorial", 1);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
