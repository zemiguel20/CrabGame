using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private HUD hud;
    [SerializeField] private GameOverPanel gameOver;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.gameObject.SetActive(true);
    }

    public void OnPlayPressed()
    {
        mainMenu.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        hud.gameObject.SetActive(true);

        GameMode gameMode = FindObjectOfType<GameMode>();
        gameMode.StartGame();
        gameMode.gameEnded += OnGameEnded;
    }

    void OnGameEnded(bool playerWon)
    {
        gameOver.gameObject.SetActive(true);
        gameOver.SetResult(playerWon);

        FindObjectOfType<GameMode>().gameEnded -= OnGameEnded;
    }

    public void OnBackPressed()
    {
        mainMenu.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);
        hud.gameObject.SetActive(false);
    }
}
