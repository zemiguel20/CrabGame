using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIDocument document;

    [TextArea]
    [SerializeField] private string instructionsText;

    public UnityEvent playPressed;

    void OnEnable()
    {
        Button playButton = document.rootVisualElement.Query<Button>("play-button");
        playButton.clicked += playPressed.Invoke;

        Button quitButton = document.rootVisualElement.Query<Button>("quit-button");
        quitButton.clicked += Application.Quit;

        Label instructions = document.rootVisualElement.Query<Label>("instructions-text");
        instructions.text = instructionsText;
    }
}
