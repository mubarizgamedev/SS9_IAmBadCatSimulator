using System;
using UnityEngine;

public class PushButtonCat : MonoBehaviour
{
    public static Action OnPushButtonClick;
    public void PushObject()
    {
        OnPushButtonClick?.Invoke();
    }
}
