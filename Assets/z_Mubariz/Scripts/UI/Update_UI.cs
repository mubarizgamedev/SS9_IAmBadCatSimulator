
using UnityEngine.UI;
using UnityEngine;

public class Update_UI : MonoBehaviour
{
    [SerializeField] Text updateText;
    [SerializeField] GameObject bgToHide;


    public void ShowTextUpdate(string text,float time)
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        updateText.text = text;
    }
}
