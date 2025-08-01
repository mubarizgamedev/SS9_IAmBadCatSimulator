using UnityEngine;
using UnityEngine.Events;

public class PotsObjective : MonoBehaviour
{
    Main_Quest Main_Quest;
    Update_UI Update_UI;
    Items_Count Items_Count;

    public int potsThrown;
    [SerializeField] int totalPotsToThrow;
    [SerializeField] string objectiveText;
    [SerializeField] AudioClip progressClip;
    [SerializeField] AudioClip objectiveComplete;
    [SerializeField] GameObject fadeGameobject;


    public UnityEvent ThingsToActivateOnEnable;


    private void OnEnable()
    {
        Invoke(nameof(Enable), 0.3f);
    }

    void Enable()
    {
        Items_Count.UpdateLevelProgress(potsThrown, totalPotsToThrow);
    }

    private void Start()
    {
       Pots.OnPotHitGranny += PickableObject_OnObjectHitGranny;
        Update_UI = FindFirstObjectByType<Update_UI>();
        Main_Quest = FindFirstObjectByType<Main_Quest>();
        Items_Count = FindFirstObjectByType<Items_Count>();

        Items_Count.UpdateLevelNumber("Level 7");
        Items_Count.UpdateLevelProgress(potsThrown, totalPotsToThrow);
        Update_UI.ShowTextUpdate(objectiveText, 10f);
        Main_Quest.UpdateMainQuest(objectiveText, potsThrown, totalPotsToThrow);
    }

    private void OnDestroy()
    {
        Pots.OnPotHitGranny -= PickableObject_OnObjectHitGranny;
    }

    private void Update()
    {
        Items_Count.UpdateLevelProgress(potsThrown, totalPotsToThrow);
        Main_Quest.UpdateMainQuest(objectiveText, potsThrown, totalPotsToThrow);
    }



    private void PickableObject_OnObjectHitGranny()
    {
        if (potsThrown < totalPotsToThrow)
        {
            potsThrown++;
            SFX_Manager.PlaySound(progressClip);
            Main_Quest.UpdateMainQuest(objectiveText, potsThrown, totalPotsToThrow);
            Items_Count.UpdateLevelProgress(potsThrown, totalPotsToThrow);

            if (potsThrown == totalPotsToThrow)
            {

                SFX_Manager.PlaySound(objectiveComplete);
               

                // OBJECTIVE COMPLETE

                PlayerPrefs.SetInt("L7", 1);
                Items_Count.UpdateLevelProgress(potsThrown, totalPotsToThrow);
                Main_Quest.UpdateMainQuest(objectiveText, potsThrown, totalPotsToThrow);
                Update_UI.ShowTextUpdate("Objective complete", 1f);
                gameObject.SetActive(false);
                fadeGameobject.SetActive(true);

                EnemyHandler.Instance.ResetState();
            }
        }
    }
}
