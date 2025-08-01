using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    [Header("Typing Config")]
    public float typingSpeed = 0.05f;         // Seconds per character
    public AudioSource audioSource;           // Audio source to play typing sound

    [Header("UI Reference")]
    public Text uiText;                       // The Text component to update
    public string textToWrite;

    private Coroutine typingCoroutine;

    private void OnEnable()
    {
        StartTyping(textToWrite);
    }

    public void StartTyping(string message)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(message));
    }

    private IEnumerator TypeText(string message)
    {
        uiText.text = "";

        foreach (char c in message)
        {
            uiText.text += c;

            yield return new WaitForSeconds(typingSpeed);
        }

        audioSource.Stop();
        typingCoroutine = null;
    }
}
