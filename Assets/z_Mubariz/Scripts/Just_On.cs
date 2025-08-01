using UnityEngine;

public class Just_On : MonoBehaviour
{
    [SerializeField] GameObject all;
    [SerializeField] float timer;

    private void OnEnable()
    {
        Invoke("Activate", timer);
    }
    void Activate()
    {
        all.SetActive(true);
    }
}