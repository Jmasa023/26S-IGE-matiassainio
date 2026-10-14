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
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void RegisterPlayer(PlayerController player)
    {
        playerController = player;
    }

    public void HandlePlayerDeath()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void RegisterDestructibleBlocks()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Destructible");
        destructibleBlocksCount = blocks.Length;
    }
    public void BlockDestroyed()
{
    destructibleBlocksCount -= 1;

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
}