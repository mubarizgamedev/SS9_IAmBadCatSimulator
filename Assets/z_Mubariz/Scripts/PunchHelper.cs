
using UnityEngine;

public class PunchHelper : MonoBehaviour
{
    public Animator grannyPunchVideo;

    public void ActNow()
    {
        grannyPunchVideo.SetBool("Punch", true);
    }
}
