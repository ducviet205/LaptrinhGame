using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleFlow : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject gameWinUI;           // ← DÒNG MỚI
    public PlayerHealth playerHealth;
    public GameObject bgMusic;

    private void Start()
    {
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);        // ← DÒNG MỚI
        playerHealth.onDead += OnGameOver;
    }

    private void Update()
    {
        if (EnemyHealth.LivingEnemyCount <= 0)   // ← Kiểm tra hết enemy
        {
            OnGameWin();
        }
    }

    private void OnGameOver()
    {
        gameOverUI.SetActive(true);
        bgMusic.SetActive(false);
    }

    private void OnGameWin()                   // ← Hàm mới
    {
        gameWinUI.SetActive(true);
        bgMusic.SetActive(false);
        playerHealth.gameObject.SetActive(false);  // tắt player
    }

    public void ReturnToMainMenu() => SceneManager.LoadScene("MainMenu");
}