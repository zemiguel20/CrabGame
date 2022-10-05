using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TimeDisplay : MonoBehaviour
{
    private GameMode gameMode;
    private TMP_Text time;

    private void OnEnable()
    {
        gameMode = FindObjectOfType<GameMode>();
        time = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (gameMode)
            time.text = gameMode.time.ToString();
    }
}
