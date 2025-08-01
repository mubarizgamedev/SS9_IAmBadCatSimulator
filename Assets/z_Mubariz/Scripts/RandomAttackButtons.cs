using System.Collections;
using UnityEngine;

public class RandomAttackButtons : MonoBehaviour
{
    public float delay = 3f; 
    void OnEnable()
    {
        StartCoroutine(ActivateChildrenInSequence());
    }

    IEnumerator ActivateChildrenInSequence()
    {
        while (true)
        {
            for (int i = 0; i < transform.childCount; i++)
            {

                for (int j = 0; j < transform.childCount; j++)
                {
                    transform.GetChild(j).gameObject.SetActive(false);
                }


                transform.GetChild(i).gameObject.SetActive(true);


                yield return new WaitForSeconds(delay);
            }
        }
    }
}
