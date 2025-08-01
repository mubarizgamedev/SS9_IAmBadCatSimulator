using UnityEngine;
using System;
using AssetKits.ParticleImage;

public class PlayerCollisionEvents : MonoBehaviour
{
    public static event Action OnCatHitBed;
    public static event Action OnCatHitObjKey;
    public static event Action OnCatHitObjQuest;

    [SerializeField] Items_Count Items_Count;


    [SerializeField] ParticleImage diamondParticleImage;
    [SerializeField] ParticleImage keyParticleImage;

    [SerializeField] string BedTag = "Bed";
    [SerializeField] string DimaondTag = "Diamonds";
    [SerializeField] string KeyTag = "Key";
    [SerializeField] string PunchTag = "Punch";
    [SerializeField] string GunTag = "Gun";
    [SerializeField] string ObjectiveKey = "ObjKey";
    [SerializeField] string Quest = "Quest";

    public static event Action OnDiamondHit;
    public static event Action OnKeyHit;
    public static event Action OnPuchBoxTrigger;
    public static event Action OnShockGunTrigger;
    [SerializeField] AudioClip keySound;
    [SerializeField] AudioClip diamondSound;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(DimaondTag))
        {
            Debug.Log("Diamond hit");
            SFX_Manager.PlaySound(diamondSound);
            other.gameObject.SetActive(false);
            OnDiamondHit?.Invoke();

            if (diamondParticleImage.gameObject.activeSelf == false)
            {
                diamondParticleImage.gameObject.SetActive(true);
                diamondParticleImage.Play();
            }
            else
            {
                diamondParticleImage.Play();
            }

            Items_Count.IncrementDiamondCount();

        }
        if (other.CompareTag(KeyTag))
        {
            Debug.Log("Key hit");
            SFX_Manager.PlaySound(keySound);
            other.gameObject.SetActive(false);
            OnKeyHit?.Invoke();
            if (keyParticleImage.gameObject.activeSelf == false)
            {
                keyParticleImage.gameObject.SetActive(true);
                keyParticleImage.Play();
            }
            else
            {
                keyParticleImage.Play();
            }
            Items_Count.IncrementKeyCount();

        }
        if (other.CompareTag(BedTag))
        {
            OnCatHitBed?.Invoke();           
        }
        if (other.CompareTag(Quest))
        {
            OnCatHitObjQuest?.Invoke();           
        }
        if (other.CompareTag(ObjectiveKey))
        {
            OnCatHitObjKey?.Invoke();
            other.gameObject.SetActive(false);
            SFX_Manager.PlaySound(keySound);
        }
        if (other.CompareTag(PunchTag))
        {
            if (SpecialItemInHand.Instance.handFreeAtMoment)
            {
                OnPuchBoxTrigger?.Invoke();
            }

        }
        if (other.CompareTag(GunTag))
        {
            if (SpecialItemInHand.Instance.handFreeAtMoment)
            {
                OnShockGunTrigger?.Invoke();
            }
        }
    }
}
