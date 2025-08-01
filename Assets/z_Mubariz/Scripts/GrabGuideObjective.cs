using UnityEngine;
using UnityEngine.Events;

public class GrabGuideObjective : MonoBehaviour
{
    [SerializeField] Transform cat;
    [SerializeField] Transform cameraTrasnform;
    [SerializeField] Transform catJumpCameraTransform;

    [Header("Pos and Rot")]
    public Vector3 targetPosition;
    public Vector3 targetPositionAfterReach;
    public float targetRotationY;
    public float targetRotationYAfterReach;

    public UnityEvent OnGuideObjectPicked;

    private void Start()
    {
        ObjectPicker.OnGuideObjectPicked += ObjectPicker_OnGuideObjectPicked;
    }

    private void ObjectPicker_OnGuideObjectPicked()
    {
        Debug.Log("Guide Object Picked");
        OnGuideObjectPicked?.Invoke();
    }

    private void OnEnable()
    {
        cat.gameObject.SetActive(false);
        cat.localPosition = targetPosition;
        cat.localRotation = Quaternion.Euler(0f, targetRotationY, 0f);
        cameraTrasnform.localEulerAngles = Vector3.zero;
        cat.gameObject.SetActive(true);         
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            cat.gameObject.SetActive(false);
            cat.localPosition = targetPositionAfterReach;
            cat.localRotation = Quaternion.Euler(0f, targetRotationYAfterReach, 0f);
            cameraTrasnform.localEulerAngles = Vector3.zero;
            cat.gameObject.SetActive(true);
        }
    }

    public void ChangePosAfterReach()
    {
        cat.gameObject.SetActive(false);
        cat.localPosition = targetPositionAfterReach;
        cat.localRotation = Quaternion.Euler(0f, targetRotationYAfterReach, 0f);
        //catJumpCameraTransform.localRotation = Quaternion.Euler(4f, 0f, 0f);
        cameraTrasnform.localRotation = Quaternion.Euler(12.5f, 0f, 0f);
        cat.gameObject.SetActive(true);
    }


}
