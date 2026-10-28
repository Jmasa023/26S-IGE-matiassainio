using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    public TextMeshProUGUI healthLabel;
    public TextMeshProUGUI blocksLabel;
    public TextMeshProUGUI levelLabel;
    public GameObject gameOverWindow;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLevelNameLabel(string name)
    {
        levelLabel.text = name;
    }

    public void SetPlayerHealthLabel(int health)
    {
        healthLabel.text = "Health: " + health;
    }

    public void SetBlockCountLabel(int count)
    {
        blocksLabel.text = "Blocks: " + count;
    }

    public void ShowGameOverWindow(bool show)
    {
        gameOverWindow.SetActive(show);
    }

    public void OnRestartClicked()
    {
        ShowGameOverWindow(false);
        GameManager.Instance.RestartLevel();
        AudioManager.Instance.PlayButtonClickSfx();
    }
}