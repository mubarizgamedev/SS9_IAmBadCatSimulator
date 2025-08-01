using UnityEngine;

public class RunFromGrannyGuide : MonoBehaviour
{
    [SerializeField] Animator guideGrannyAnimator;

    private void OnEnable()
    {
        guideGrannyAnimator.SetBool("AngerStart", true);
        Sound();
        PlayerPrefs.SetInt("Tutorial", 1);
    }

    private void Sound()
    {
        //int selectedIndex = PlayerPrefs.GetInt("SelectedGrannyIndex", 0);

      

        //if (selectedIndex == 0 || selectedIndex == 3)
        //{
        //    SFX_Manager.PlayRandomSound(SFX_Manager.Instance.angryTalkGrandpa);
        //}
        //else
        
            SFX_Manager.PlaySound(SFX_Manager.Instance.angryTalkGranny);
        //}
    }
}
