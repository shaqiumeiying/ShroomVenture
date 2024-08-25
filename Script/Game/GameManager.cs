using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private UI_InGame inGameUI;

    public Piggy piggy;

    [Header("Level Management")]
    [SerializeField] private int currentLevelIndex;



    [Header("Player")]
    [SerializeField] private GameObject piggyPrefab;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay;

    [Header("ShroomManager")]
    public int basicShroomCollected;
    public int totalShrooms;

    public BasicShroom[] allShrooms;

    [Header("CheckPoints")]
    public bool canReactivate;

    [Header("Traps")]
    public GameObject arrowPrefab;

    [SerializeField] private float levelTimer;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

    }

    private void Start()
    {
        inGameUI = UI_InGame.instance;

        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        CollectBasicShroomInfo();
        

    }

    private void Update()
    {
        levelTimer += Time.deltaTime;

        inGameUI.UpdateTimerUI(levelTimer);
    }

    private void CollectBasicShroomInfo()
    {
        allShrooms = FindObjectsByType<BasicShroom>(FindObjectsSortMode.None);
        totalShrooms = allShrooms.Length;
        inGameUI.UpdateCoolectibleUI(basicShroomCollected, totalShrooms);
    }

    public void UpdateRespawnPosition(Transform newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }

    public void RespawnPiggy()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        GameObject newPiggy = Instantiate(piggyPrefab, respawnPoint.position, quaternion.identity); 
        piggy = newPiggy.GetComponent<Piggy>();
    }

    public void AddShroom()
    {
        basicShroomCollected++;
        inGameUI.UpdateCoolectibleUI(basicShroomCollected, totalShrooms);

    }

    public void CreateObject(GameObject prefab, Transform target, float delay = 0)
    {
        StartCoroutine(CreateObjectCoroutine(prefab, target, delay));
        
    }
    private IEnumerator CreateObjectCoroutine(GameObject prefab, Transform target, float delay){
        
        Vector3 newPosition = target.position;
        
        yield return new WaitForSeconds(delay);
        
        
        GameObject newObject = Instantiate(prefab, newPosition, Quaternion.identity);

    }

    private void TheEnd() => SceneManager.LoadScene("End");

    private void LoadNextLevel()
    {
        int nextLevelIndex = currentLevelIndex + 1;

        SceneManager.LoadScene("Level" + nextLevelIndex);
        Debug.Log("next level is: " + nextLevelIndex);
    }

    public void LevelFinished()
    {
        FadeScene fadeEffect = UI_InGame.instance.fadeEffect;

        int lastLevelIndex = SceneManager.sceneCountInBuildSettings - 2; // excluuding main menu and the credit scene
        bool noMoreLevels = currentLevelIndex  == lastLevelIndex;

        if (noMoreLevels)
        {
            Debug.Log("no More Levels!");
            fadeEffect.ScreenFade(1, 1.5f, TheEnd);
        }
        else
        {
            Debug.Log("More Levels Ahead");
            fadeEffect.ScreenFade(1, 1.5f, LoadNextLevel);
        }
     
    }

}
