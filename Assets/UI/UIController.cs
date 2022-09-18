using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    private Label pressLabel;
    private Label gameoverLabel;
    private Label instructionsLabel;
    private Label timeLabel;

    [SerializeField] private Gamemode gamemode;

    private void Awake()
    {
        pressLabel = document.rootVisualElement.Query<Label>("pressLabel");
        gameoverLabel = document.rootVisualElement.Query<Label>("gameoverLabel");
        instructionsLabel = document.rootVisualElement.Query<Label>("instructionsLabel");
        timeLabel = document.rootVisualElement.Query<Label>("timeLabel");
    }

    void Update()
    {
        pressLabel.visible = (gamemode.state == Gamemode.GameState.START
                               || gamemode.state == Gamemode.GameState.GAMEOVER);

        gameoverLabel.visible = (gamemode.state == Gamemode.GameState.GAMEOVER);

        instructionsLabel.visible = (gamemode.state == Gamemode.GameState.START);

        timeLabel.visible = (gamemode.state == Gamemode.GameState.RUNNING
                               || gamemode.state == Gamemode.GameState.GAMEOVER);
        timeLabel.text = gamemode.time.ToString("F2");
    }
}
