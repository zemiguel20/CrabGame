using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private UIDocument document;

    public UnityEvent backPressed;
    public UnityEvent playAgainPressed;

    private void OnEnable()
    {
        Button backButton = document.rootVisualElement.Query<Button>("back-button");
        backButton.clicked += backPressed.Invoke;

        Button playAgainButton = document.rootVisualElement.Query<Button>("play-again-button");
        playAgainButton.clicked += playAgainPressed.Invoke;
    }

    public void SetResult(bool playerWon)
    {
        Label resultLabel = document.rootVisualElement.Query<Label>("result-label");
        resultLabel.text = playerWon ? "You Won" : "You Lost";
        resultLabel.style.color = playerWon ? Color.green : Color.red;
    }
}
