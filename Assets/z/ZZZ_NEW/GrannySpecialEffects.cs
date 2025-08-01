using UnityEngine;

public class GrannySpecialEffects : MonoBehaviour
{
    [SerializeField] GameObject beeEffect;
    [SerializeField] GameObject fireEffect;
    [SerializeField] GameObject currentEffect;
    

    //ANIMATION EVENTS FOR SPECIAL ATTACK EFFECTS
    public void OnBeeEffect()
    {
        beeEffect.SetActive(true);
    }
   
    public void OnFireEffect()
    {
        fireEffect.SetActive(true);
    }
   
    public void OnCurrentEffect()
    {
        currentEffect.SetActive(true);
    }
   
}
