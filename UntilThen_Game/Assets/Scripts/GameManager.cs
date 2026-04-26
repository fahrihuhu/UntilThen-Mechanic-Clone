using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // C# Actions & Delegates
    // Memancarkan sinyal (event) ke skrip lain tanpa perlu tahu skrip apa yang menerimanya
    public static event Action<bool> OnGamePaused;
    public static event Action OnGameStart;
    public static event Action OnReturnToMenu;

    private bool isPaused = false;

    public void PlayGame()
    {
        Time.timeScale = 1f; // Waktu berjalan normal
        isPaused = false;
        OnGameStart?.Invoke(); 
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Waktu berhenti
        OnGamePaused?.Invoke(isPaused); 
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; 
        OnGamePaused?.Invoke(isPaused);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        OnReturnToMenu?.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Exited!"); // Cuma muncul di console editor
    }

    void Update()
    {
        // Tekan ESC untuk pause/resume (Opsional tapi berguna buat testing)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }
}