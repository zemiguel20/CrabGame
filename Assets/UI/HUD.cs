using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    [SerializeField] private UIDocument document;

    private Label timeLabel;
    private GameMode gameMode;

    void OnEnable()
    {
        timeLabel = document.rootVisualElement.Query<Label>("time-label");
        gameMode = FindObjectOfType<GameMode>();
    }

    void Update()
    {
        timeLabel.text = gameMode.time.ToString();
    }
}
