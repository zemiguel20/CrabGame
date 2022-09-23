using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    [SerializeField] private UIDocument document;

    [SerializeField] private GameMode gameMode;

    private Label timeLabel;

    private void OnEnable()
    {
        timeLabel = document.rootVisualElement.Query<Label>("time-label");
    }

    private void Update()
    {
        timeLabel.text = gameMode.time.ToString("F0");
    }
}
