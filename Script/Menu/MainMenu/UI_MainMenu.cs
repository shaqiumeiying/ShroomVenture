using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements;
    private FadeScene fadeEffect;
    
    public string FirstLevelName;

    private void Awake()
    {
        fadeEffect = GetComponentInChildren<FadeScene>();
    }

    private void Start()
    {
        fadeEffect.ScreenFade(0, 1.5f);
    }

    public void SwitchUI(GameObject uiToEnable)
    {
        foreach (GameObject ui in uiElements)
        {
            ui.SetActive(false);
        }

        uiToEnable.SetActive(true);
    }




    public void NewGame()
    {
        fadeEffect.ScreenFade(1, 1.5f, LoadLevelScene);
    }

    private void LoadLevelScene() => SceneManager.LoadScene(FirstLevelName);
}
