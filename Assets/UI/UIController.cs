using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        settingsPanel.SetActive(false);
        hud.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void OnPlayPressed()
    {
        mainMenu.SetActive(false);
        gameOverPanel.SetActive(false);
        hud.SetActive(true);

        GameMode gameMode = FindObjectOfType<GameMode>();
        gameMode.StartGame();
        gameMode.gameEnded += OnGameEnded;
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }

    public void OnSettingsPressed()
    {
        mainMenu.SetActive(false);
        settingsPanel.SetActive(true);
    }

    void OnGameEnded(bool playerWon)
    {
        gameOverPanel.SetActive(true);
        gameOverPanel.GetComponentInChildren<ResultMessage>().SetResult(playerWon);

        FindObjectOfType<GameMode>().gameEnded -= OnGameEnded;
    }

    public void OnBackPressed()
    {
        mainMenu.SetActive(true);
        settingsPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        hud.SetActive(false);
    }
}
