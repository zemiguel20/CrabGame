using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIDocument document;

    public UnityEvent playPressed;
    public UnityEvent quitPressed;

    void OnEnable()
    {
        Button playButton = document.rootVisualElement.Query<Button>("play-button");
        playButton.clicked += playPressed.Invoke;

        Button quitButton = document.rootVisualElement.Query<Button>("quit-button");
        quitButton.clicked += quitPressed.Invoke;
    }

    private void OnDisable()
    {
        Button playButton = document.rootVisualElement.Query<Button>("play-button");
        playButton.clicked -= playPressed.Invoke;

        Button quitButton = document.rootVisualElement.Query<Button>("quit-button");
        quitButton.clicked -= quitPressed.Invoke;
    }
}
