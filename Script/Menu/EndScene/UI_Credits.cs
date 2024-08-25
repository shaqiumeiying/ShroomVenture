using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Credits : MonoBehaviour
{
    [SerializeField] private RectTransform rectT;
    [SerializeField] private float scrollSpeed = 200;

    [SerializeField] private string mainMenueSceneName = "MainMenu";
    [SerializeField] private float textEndPosition = 1000;
    private bool creditSkipped;
    private FadeScene fadeEffect;

    private void Awake()
    {
        fadeEffect = GetComponentInChildren<FadeScene>();
        fadeEffect.ScreenFade(0, 1);
    }

    private void Update()
    {
        rectT.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (rectT.anchoredPosition.y >= textEndPosition)
        {
            GotoMainMenu();
        }
    }

    public void SkipCredits()
    {
        if (creditSkipped == false)
        {
            scrollSpeed *= 10;
            creditSkipped = true;
        }
        else
        {
            GotoMainMenu();
        }
        
    }

    private void GotoMainMenu() => fadeEffect.ScreenFade(1, 1, GotoMainMenuScene);

    private void GotoMainMenuScene()
    {
        SceneManager.LoadScene(mainMenueSceneName);
    }
}
