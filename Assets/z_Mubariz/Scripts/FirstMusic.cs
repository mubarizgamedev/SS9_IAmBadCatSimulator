using UnityEngine;

public class FirstMusic : MonoBehaviour
{
    private static FirstMusic instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            // Destroy duplicate if one already exists
            Destroy(gameObject);
            return;
        }

        // Make this the instance and persist across scenes
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Destroys the persistent FirstMusic instance.
    /// </summary>
    public void DestroySelf()
    {
        if (instance == this)
        {
            instance = null;
            Destroy(gameObject);
        }
    }
}
