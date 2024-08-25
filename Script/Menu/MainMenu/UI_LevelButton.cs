using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LevelButton : MonoBehaviour
{
    public string sceneName;
    private int levelIndex;

    public void SetupButton(int newlevelIndex)
    {
        levelIndex = newlevelIndex;

        sceneName = "Level" + levelIndex;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }
}
