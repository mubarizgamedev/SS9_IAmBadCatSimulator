using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public GameObject grannyModel;
    public GameObject grandpModel;
    public Animator animator;
    public Avatar grannyAvatar;
    public Avatar grandpaAvatar;

    private void OnEnable()
    {
        Invoke(nameof(ChangeCharacterAccordicngly), 0.15f);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();       
    }

    private void ChangeCharacterAccordicngly()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedGrannyIndex", 0);

        Debug.Log("Selected enemy idex is :" + selectedIndex);

        if (selectedIndex == 0 || selectedIndex == 3)
        {
            grandpModel.SetActive(true);
            grannyModel.SetActive(false);
            animator.avatar = grandpaAvatar;
        }
        else
        {
            grandpModel.SetActive(false);
            grannyModel.SetActive(true);
            animator.avatar = grannyAvatar;
        }
    }
}
