using UnityEngine;

public class ObjectTargetAtPlayer : MonoBehaviour
{
    private static ObjectTargetAtPlayer _instance;

    public static ObjectTargetAtPlayer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObjectTargetAtPlayer>();

                if (_instance == null)
                {
                    Debug.LogWarning("Another isntance of class 'ObjectTargetAtPlayer' is present");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // Prevent duplicate instances
            return;
        }

        _instance = this;
    }

    }
