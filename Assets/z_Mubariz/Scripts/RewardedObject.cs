using UnityEngine;

public class RewardedObject : MonoBehaviour
{

    [SerializeField] LayerMask interactLayer;
    

    public bool canBeRewarded;

    private void Start()
    {
        canBeRewarded = true;
    }



}
