using System.Collections.Generic;
using UnityEngine;

public class ObjectsPosReseter : MonoBehaviour
{
    private static List<(Transform objTransform, Vector3 startPosition, Quaternion startRotation,GameObject objGameObject)> objectsData = new();

    private void Awake()
    {
        FindAllPickableObjects();
    }

    /// <summary>
    /// Finds all objects with the given tag and stores their initial transforms.
    /// </summary>
    //private void FindAllPickableObjects()
    //{
    //    objectsData.Clear(); // Clear old data
    //    GameObject[] pickableObjects = GameObject.FindGameObjectsWithTag(pickableTag);

    //    foreach (GameObject obj in pickableObjects)
    //    {
    //        objectsData.Add((obj.transform, obj.transform.position, obj.transform.rotation,obj));
    //    }
    //}

    private void FindAllPickableObjects()
    {
        objectsData.Clear(); // Clear old data
        GameObject[] allObjects = FindObjectsOfType<GameObject>(); // Get all scene objects

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == 8) // 8 = Interactible layer
            {
                objectsData.Add((obj.transform, obj.transform.position, obj.transform.rotation, obj));
            }
        }
    }


    /// <summary>
    /// Resets all stored objects to their original position and rotation.
    /// </summary>
    public static void ResetAllObjects()
    {
        foreach (var data in objectsData)
        {
            data.objTransform.position = data.startPosition;
            data.objTransform.rotation = data.startRotation;
            data.objGameObject.SetActive(true);
            Debug.Log("position reset");
        }
    }

    [ContextMenu("Reset all object position")]

    public void Reset()
    {
        ResetAllObjects();
    }
}
