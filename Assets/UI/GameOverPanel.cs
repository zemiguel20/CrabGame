using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private UIDocument document;

    public UnityEvent continuePressed;

    private void OnEnable()
    {
        Button backButton = document.rootVisualElement.Query<Button>("continue-button");
        backButton.clicked += continuePressed.Invoke;
    }

    public void SetResult(bool playerWon)
    {
        Label resultLabel = document.rootVisualElement.Query<Label>("result-label");
        resultLabel.text = playerWon ? "You Won" : "You Lost";
        resultLabel.style.color = playerWon ? Color.green : Color.red;
    }

    private void OnDisable()
    {
        Button backButton = document.rootVisualElement.Query<Button>("continue-button");
        backButton.clicked -= continuePressed.Invoke;
    }
}
