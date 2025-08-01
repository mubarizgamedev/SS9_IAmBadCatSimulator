using UnityEngine;

public class RemveEffect : MonoBehaviour
{
    [SerializeField] GameObject effect;
    private void OnDisable()
    {
        effect.SetActive(false);
    }
}
