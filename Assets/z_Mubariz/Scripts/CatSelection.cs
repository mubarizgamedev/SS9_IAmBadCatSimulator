using UnityEngine;
using UnityEngine.UI;

public class PetSelection : MonoBehaviour
{
    public GameObject[] cats; // Array of cat models or images
    public int currentIndex = 0; // Current selected cat
    public Button unlockButton; // UI Button for unlocking cats
    public Button selectButton; // UI Button to select cat

    void Start()
    {
        currentIndex = PlayerPrefs.GetInt("SelectedCatIndex", 0);
        UpdateCatDisplay();
    }

    public void MoveLeft()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = cats.Length - 1; // Loop to last cat
        }
        UpdateCatDisplay();
    }

    public void MoveRight()
    {
        currentIndex++;
        if (currentIndex >= cats.Length)
        {
            currentIndex = 0; // Loop to first cat
        }
        UpdateCatDisplay();
    }

    public void SelectCat()
    {
        if (IsCatUnlocked(currentIndex))
        {
            PlayerPrefs.SetInt("SelectedCatIndex", currentIndex);
            PlayerPrefs.Save();
            Debug.Log("Cat " + currentIndex + " selected!");
        }
        else
        {
            Debug.Log("This cat is locked! Unlock it first.");
        }
    }

    public void UnlockCat()
    {
        PlayerPrefs.SetInt("CatUnlocked_" + currentIndex, 1);
        PlayerPrefs.Save();
        Debug.Log("Cat " + currentIndex + " unlocked!");
        UpdateCatDisplay();
    }

    void UpdateCatDisplay()
    {
        for (int i = 0; i < cats.Length; i++)
        {
            cats[i].SetActive(i == currentIndex); // Show only the selected cat
        }

        if (IsCatUnlocked(currentIndex))
        {
            unlockButton.gameObject.SetActive(false); // Hide unlock button if cat is unlocked
            selectButton.interactable = true; // Allow selection
        }
        else
        {
            unlockButton.gameObject.SetActive(true); // Show unlock button
            selectButton.interactable = false; // Prevent selection
        }
    }

    bool IsCatUnlocked(int index)
    {
        if (index == 0 || index == 1) return true; // First two cats are always unlocked
        return PlayerPrefs.GetInt("CatUnlocked_" + index, 0) == 1; // Check if cat is unlocked
    }
}
