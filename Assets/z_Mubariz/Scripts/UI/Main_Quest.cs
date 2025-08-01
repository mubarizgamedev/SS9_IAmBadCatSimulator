using UnityEngine.UI;
using UnityEngine;

public class Main_Quest : MonoBehaviour
{
    [SerializeField] Text m_ObjectiveText;
    [SerializeField] Text m_CurrentText;
    [SerializeField] Text m_TotalText;

    public void UpdateMainQuest(string objectiveText, int currentText, int totalText)
    {
        m_ObjectiveText.text = objectiveText;
        m_CurrentText.text = currentText.ToString();
        m_TotalText.text = totalText.ToString();
    }
}
