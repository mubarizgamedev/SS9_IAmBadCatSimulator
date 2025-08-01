using UnityEngine;

public class CameraFreezZ : MonoBehaviour
{
    public static CameraFreezZ Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("Multiple instances of CameraFreezZ detected. Destroying the new instance.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
    }
}
