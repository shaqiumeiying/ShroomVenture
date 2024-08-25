using TMPro;
using UnityEngine;

public class UI_InGame : MonoBehaviour
{
    public static UI_InGame instance;

    public FadeScene fadeEffect { get; private set; }

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI collectibleText;

    private void Awake()
    {
        instance = this;

        fadeEffect = GetComponentInChildren<FadeScene>();
    }

    private void Start()
    {
        fadeEffect.ScreenFade(0, 1);
    }

    public void UpdateCoolectibleUI(int collected, int total)
    {
        collectibleText.text = collected + "/" + total;
    }

    public void UpdateTimerUI(float timer)
    {
        timerText.text = timer.ToString("00") + " s";
    }
}
