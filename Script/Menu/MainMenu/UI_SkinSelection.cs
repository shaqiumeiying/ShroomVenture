using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_SkinSelection : MonoBehaviour
{
    [SerializeField] private int currentIndex;
    [SerializeField] private int maxIndex;
    [SerializeField] private Animator skinDisplay;
    [SerializeField] private TextMeshProUGUI selectText;
    [SerializeField] private Button selectButton;

    public void NextSkin()
    {
        currentIndex++;

        if (currentIndex > maxIndex)
            currentIndex = 0;

        UpdateSkinDisplay();
    }

    public void PrevSkin()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = maxIndex;

        UpdateSkinDisplay();
    }

    private void UpdateSkinDisplay()
    {
        for (int i = 0; i < skinDisplay.layerCount; i++)
        {
            skinDisplay.SetLayerWeight(i, 0);
        }

        skinDisplay.SetLayerWeight(currentIndex, 1);

        if (currentIndex == 1 || currentIndex == 2)
        {
            selectText.text = "???";
            selectButton.interactable = false;
        }
        else
        {
            selectText.text = "Select";
            selectButton.interactable = true;
        }
    }
}
