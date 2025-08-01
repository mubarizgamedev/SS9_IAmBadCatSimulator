using System;
using UnityEngine;
using UnityEngine.UI;   

public class CatScratch : MonoBehaviour
{
    public Button scratchButton;
    public static event Action OnScratchButtonClickedEvent;

    void Start()
    {
        scratchButton.onClick.AddListener(OnScratchButtonClicked);
    }
    void OnDestroy()
    {
        scratchButton.onClick.RemoveListener(OnScratchButtonClicked);
    }

    void OnScratchButtonClicked()
    {
        OnScratchButtonClickedEvent?.Invoke();
    }
}
