using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class DynamicDifficultyPicker : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private List<GameDifficulty> difficultyLevels;
    [SerializeField] private int startDifficultyLevel;

    private int winCount;
    private int lossCount;
    private int currentDifficultyLevel;
    private List<bool> currentDifficultyResults;

    private void OnValidate()
    {
        startDifficultyLevel = Mathf.Clamp(startDifficultyLevel, 0, difficultyLevels.Count - 1);
    }

    private void Awake()
    {
        winCount = 0;
        lossCount = 0;
        currentDifficultyLevel = startDifficultyLevel;
        currentDifficultyResults = new List<bool>();
    }

    private void Start()
    {
        gameMode.currentDifficulty = difficultyLevels[startDifficultyLevel];
        gameMode.gameEnded += OnGameEnd;
    }

    void OnGameEnd(bool playerWon)
    {
        // Save result

        // Add to global count
        if (playerWon) { winCount++; }
        else { lossCount++; }
        // Add result to current difficulty history
        currentDifficultyResults.Add(playerWon);

        // Analyse player stats and decide difficulty

        // if 6+ games played on current difficulty then analyze
        if (currentDifficultyResults.Count >= 6)
        {
            // Get win count in last 6 games
            int recentWinCount = currentDifficultyResults.
                Skip(currentDifficultyResults.Count - 6).
                Where(res => res == true).
                Count();

            // if not hardest difficulty and 5+ wins then increase difficulty
            bool needIncrease = currentDifficultyLevel < (difficultyLevels.Count - 1) && recentWinCount >= 5;
            // if not easiest difficulty and 1- wins then decrease difficulty
            bool needDecrease = currentDifficultyLevel > 0 && recentWinCount <= 1;

            if (needIncrease || needDecrease)
            {
                if (needIncrease) currentDifficultyLevel++;
                if (needDecrease) currentDifficultyLevel--;
                gameMode.currentDifficulty = difficultyLevels[currentDifficultyLevel];
                currentDifficultyResults.Clear();
            }
        }

    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        StringBuilder info = new StringBuilder();
        info.AppendLine("Total wins: " + winCount);
        info.AppendLine("Total losses: " + lossCount);
        info.AppendLine("Current Difficulty Level: " + currentDifficultyLevel);

        // Show last 6 games
        StringBuilder history = new StringBuilder();
        int startInd = currentDifficultyResults.Count - 6;
        if (startInd < 0) startInd = 0;
        for (int i = startInd; i < currentDifficultyResults.Count; i++)
        {
            if (currentDifficultyResults[i])
                history.Append("W");
            else
                history.Append("L");
        }
        info.AppendLine(history.ToString());

        GUI.skin.box.fontSize = 15;
        GUI.skin.box.fontStyle = FontStyle.Bold;
        GUI.Box(new Rect(20, 20, 200, 80), info.ToString());
    }
#endif
}
