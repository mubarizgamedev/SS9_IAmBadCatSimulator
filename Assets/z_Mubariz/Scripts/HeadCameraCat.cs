using System.Collections;
using UnityEngine;

public class HeadCameraCat : MonoBehaviour
{
    public float tiltAngleX = 5f; // Amount of rotation to apply on the X-axis
    public float smoothSpeed = 2f; // Speed of smooth rotation
    private bool rotating = false; // To track if the camera is rotating
    private Animator animator; // Reference to the Animator component

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Get input values for movement
        float moveX = ControlFreak2.CF2Input.GetAxis("Vertical");
        float moveY = ControlFreak2.CF2Input.GetAxis("Horizontal");

        // Check if moveX or moveY are greater than zero and start rotating
        if ((moveX > 0 || moveY > 0) && !rotating)
        {
            animator.SetBool("move", true);
        }

        // If both moveX and moveY are zero, stop rotating
        if (moveX == 0 && moveY == 0)
        {
            animator.SetBool("move", false);
        }
    }

    
}
