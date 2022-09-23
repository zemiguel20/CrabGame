using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class DifficultyMenu : MonoBehaviour
{
    [SerializeField] private UIDocument document;

    public UnityEvent easyPressed;
    public UnityEvent mediumPressed;
    public UnityEvent hardPressed;
    public UnityEvent backPressed;

    private void OnEnable()
    {
        Button easyButton = document.rootVisualElement.Query<Button>("easy-button");
        easyButton.clicked += easyPressed.Invoke;

        Button mediumButton = document.rootVisualElement.Query<Button>("medium-button");
        mediumButton.clicked += mediumPressed.Invoke;

        Button hardButton = document.rootVisualElement.Query<Button>("hard-button");
        hardButton.clicked += hardPressed.Invoke;

        Button backButton = document.rootVisualElement.Query<Button>("back-button");
        backButton.clicked += backPressed.Invoke;
    }

    private void OnDisable()
    {
        Button easyButton = document.rootVisualElement.Query<Button>("easy-button");
        easyButton.clicked -= easyPressed.Invoke;

        Button mediumButton = document.rootVisualElement.Query<Button>("medium-button");
        mediumButton.clicked -= mediumPressed.Invoke;

        Button hardButton = document.rootVisualElement.Query<Button>("hard-button");
        hardButton.clicked -= hardPressed.Invoke;

        Button backButton = document.rootVisualElement.Query<Button>("back-button");
        backButton.clicked -= backPressed.Invoke;
    }
}
