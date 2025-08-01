using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ThrowGuideObjective : MonoBehaviour
{
    [SerializeField] Transform cat;
    [SerializeField] Transform cameraTrasnform;

    [Header("Pos and Rot")]
    public Vector3 targetPosition;
    public float targetRotationY;


    public UnityEvent OnObjectHitGranny;
    [SerializeField] Image throwImage;
    [SerializeField] Image green;

    
    private void Start()
    {
        PickableObject.OnGuideObjectHitGranny += PickableObject_OnGuideObjectHitGranny;
        TouchCheck.OnTouch += TouchCheck_OnTouch;
    }
    private void OnDestroy()
    {
        PickableObject.OnGuideObjectHitGranny -= PickableObject_OnGuideObjectHitGranny;
        TouchCheck.OnTouch -= TouchCheck_OnTouch;
    }

    private void TouchCheck_OnTouch()
    {
        Invoke(nameof(AlphaOne), 1f);
    }

    void AlphaOne()
    {
        SetAlpha(throwImage, 1);
        SetAlpha(green, 1);
    }

    private void PickableObject_OnGuideObjectHitGranny()
    {        
        OnObjectHitGranny?.Invoke();
    }

    private void OnEnable()
    {
        SetAlpha(throwImage, 0);
        SetAlpha(green, 0);
        cat.gameObject.SetActive(false);
        //cat.localPosition = targetPosition;
        //cat.localRotation = Quaternion.Euler(0f, targetRotationY, 0f);
        //cameraTrasnform.localEulerAngles = Vector3.zero;
        cat.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            SetAlpha(throwImage, 0);
            SetAlpha(green, 0);
            cat.gameObject.SetActive(false);
            //cat.localPosition = targetPosition;
            //cat.localRotation = Quaternion.Euler(0f, targetRotationY, 0f);
            //cameraTrasnform.localEulerAngles = Vector3.zero;
            cat.gameObject.SetActive(true);
        }
    }

    void SetAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

}
