using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject pauseMenuPanel;

    private void OnEnable()
    {
        // Komunikasi / Observer Pattern
        // UIManager "berlangganan" ke sinyal yang dipancarkan GameManager
        GameManager.OnGameStart += HideMainMenu;
        GameManager.OnGamePaused += TogglePauseMenu;
        GameManager.OnReturnToMenu += ShowMainMenu;
    }

    private void OnDisable()
    {
        // Wajib "berhenti berlangganan" saat objek mati agar tidak memory leak
        GameManager.OnGameStart -= HideMainMenu;
        GameManager.OnGamePaused -= TogglePauseMenu;
        GameManager.OnReturnToMenu -= ShowMainMenu;
    }

    private void Start()
    {
        ShowMainMenu(); // Saat game baru buka, pastikan Main Menu yang muncul
    }

    private void HideMainMenu()
    {
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
    }

    private void TogglePauseMenu(bool isPaused)
    {
        pauseMenuPanel.SetActive(isPaused);
    }

    private void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
    }
}