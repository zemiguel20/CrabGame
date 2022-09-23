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
        Label wonLabel = document.rootVisualElement.Query<Label>("won-label");
        wonLabel.visible = playerWon;

        Label lostLabel = document.rootVisualElement.Query<Label>("lost-label");
        lostLabel.visible = !playerWon;
    }

    private void OnDisable()
    {
        Button backButton = document.rootVisualElement.Query<Button>("continue-button");
        backButton.clicked -= continuePressed.Invoke;
    }
}
