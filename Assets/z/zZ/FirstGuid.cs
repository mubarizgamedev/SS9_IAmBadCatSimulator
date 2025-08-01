using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstGuid : MonoBehaviour
{
    [SerializeField] float timeToDisable = 5f;
    bool canDisable = true;
    private void OnEnable()
    {
        TouchCheck.OnTouch += TouchCheck_OnTouch;
    }

    private void OnDisable()
    {
        TouchCheck.OnTouch -= TouchCheck_OnTouch;
    }

    private void TouchCheck_OnTouch()
    {
        Invoke(nameof(DisableGameobject), timeToDisable);
    }

    void DisableGameobject()
    {
        if (canDisable)
        {
            if (gameObject != null)
                gameObject.SetActive(false);

            canDisable = false;
        }
        
    }

    public void LoadSceneAgain()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
