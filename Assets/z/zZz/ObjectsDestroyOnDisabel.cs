using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsDestroyOnDisabel : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToDestroy;

    void Dstroy()
    {
        foreach (GameObject obj in objectsToDestroy)
        {
            if (obj != null)
            {

            Destroy(obj);
            }
        }
    }

    private void OnDisable()
    {
        Dstroy();
    }
}
