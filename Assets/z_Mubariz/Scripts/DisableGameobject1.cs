using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameobject1 : MonoBehaviour
{
    [SerializeField] GameObject gb;

    public void DisableGameobject()
    {
        gb.SetActive(false);
        SpecialItemInHand.Instance.SetItemState(gameObject, false);
    }
}
