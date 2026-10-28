using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private PlayerController playerController;
    private int destructibleBlocksCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Debug.LogWarning("Duplicate GameManager");
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        RegisterDestructibleBlocks();
        GameUI.Instance.SetLevelNameLabel(scene.name);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void RegisterPlayer(PlayerController player)
    {
        playerController = player;
        AudioManager.Instance.PlayGameplayMusic();
    }

    public void HandlePlayerDeath()
    {
        GameUI.Instance.ShowGameOverWindow(true);
        AudioManager.Instance.PlayGameOverMusic();
    }
    private void RegisterDestructibleBlocks()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Destructible");
        destructibleBlocksCount = blocks.Length;
        GameUI.Instance.SetBlockCountLabel(destructibleBlocksCount);
    }
    public void BlockDestroyed()

    {
        destructibleBlocksCount -= 1;
        GameUI.Instance.SetBlockCountLabel(destructibleBlocksCount);
        
        if (destructibleBlocksCount <= 0)
        {
            HandlePlayerWin();
        }
    }

    public void HandlePlayerWin()
    {
        if (playerController == null || playerController.health <= 0)
        {
            return;
        }

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Yessir you did it");
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}