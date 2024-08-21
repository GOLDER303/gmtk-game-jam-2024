using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int TimeLeftToWin { private set; get; } = 3 * 60;

    public Action OnGameWon;

    [SerializeField]
    private PlayerHealthManager playerHealthManager;

    [SerializeField]
    private GameObject playerGameObject;

    [SerializeField]
    private GameObject gui;

    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private GameObject youWonScreen;

    [SerializeField]
    private TextMeshProUGUI countdownText;

    private void OnEnable()
    {
        playerHealthManager.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        playerHealthManager.OnPlayerDeath -= OnPlayerDeath;
    }

    private void Start()
    {
        StartCoroutine(CountDownCoroutine());
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

    private IEnumerator CountDownCoroutine()
    {
        while (true)
        {
            if (TimeLeftToWin <= 0)
            {
                HandleGameWon();
            }

            yield return new WaitForSeconds(1);

            TimeLeftToWin -= 1;

            int minutesLeft = TimeLeftToWin / 60;
            int secondsLeft = TimeLeftToWin - minutesLeft * 60;

            string secondsLeftString =
                secondsLeft < 10 ? $"0{secondsLeft}" : secondsLeft.ToString();

            countdownText.text = $"{minutesLeft}:{secondsLeftString}";
        }
    }

    private void HandleGameWon()
    {
        countdownText.text = "";
        youWonScreen.SetActive(true);
        playerGameObject.GetComponent<PlayerInput>().DeactivateInput();
        OnGameWon?.Invoke();
    }
}
