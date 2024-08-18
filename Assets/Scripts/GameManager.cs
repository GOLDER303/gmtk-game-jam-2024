using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerHealthManager playerHealthManager;

    [SerializeField]
    private GameObject gui;

    [SerializeField]
    private GameObject gameOverScreen;

    private void OnEnable()
    {
        playerHealthManager.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        playerHealthManager.OnPlayerDeath -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        gui.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
