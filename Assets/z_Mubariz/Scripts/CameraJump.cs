using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class CameraJump : MonoBehaviour
{
    private Animator m_Animator;
    [SerializeField] float m_JumpHeight = 0.08f;
    [SerializeField] float m_JumpDuration = 0.15f;
    [SerializeField] float resaetDuration = 0.15f;
    [SerializeField] Vector3 jumpCameraTargetVector;


    private void Start()
    {
        FirstPersonController.OnLandOnGround += OnCatLanded;
        m_Animator = GetComponent<Animator>();
    }

    private void OnCatLanded()
    {
        //Debug.Log("Cat landed.........");
        StartBounce();
        Invoke(nameof(Fix), resaetDuration);
    }

    public void JumpFalse()
    {
        m_Animator.SetBool("jump", false);
    }

    private void OnDestroy()
    {
        FirstPersonController.OnLandOnGround -= OnCatLanded;
    }

    public void StartBounce()
    {
        StartCoroutine(MoveYBounce(transform, m_JumpHeight, m_JumpDuration));
    }

    IEnumerator MoveYBounce(Transform target, float deltaY, float duration)
    {
        Vector3 originalPosition = target.localPosition;
        Vector3 targetPosition = new Vector3(originalPosition.x, originalPosition.y + deltaY, originalPosition.z);

        // Move up
        float elapsed = 0f;
        while (elapsed < duration)
        {
            target.localPosition = Vector3.Slerp(originalPosition, targetPosition, elapsed / duration);
            //Debug.Log("Lerping to: " + target.localPosition);
            elapsed += Time.deltaTime;
            yield return null;
        }
        target.localPosition = targetPosition;

        // Move back
        elapsed = 0f;
        while (elapsed < duration)
        {
            target.localPosition = Vector3.Slerp(targetPosition, originalPosition, elapsed / duration);
            //Debug.Log("Lerping back to: " + target.localPosition);
            elapsed += Time.deltaTime;
            yield return null;
        }
        target.localPosition = originalPosition;
    }

    void Fix()
    {
        gameObject.SetActive(false);
        transform.localPosition = jumpCameraTargetVector;
        gameObject.SetActive(true);
    }
}
