using UnityEngine;

public class DisableGameObject : MonoBehaviour
{
    [SerializeField] GameObject parentGameobject;
    [SerializeField] GameObject objectiveManger;
    public void DisableParentGameObject()
    {
        parentGameobject.SetActive(false);
        objectiveManger.SetActive(true);
    }
    
}
