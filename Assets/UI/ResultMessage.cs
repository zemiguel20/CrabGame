using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class ResultMessage : MonoBehaviour
{
    public void SetResult(bool playerWon)
    {
        TMP_Text message = GetComponent<TMP_Text>();
        message.text = playerWon ? "You Won" : "You Lost";
        message.color = playerWon ? Color.green : Color.red;
    }
}
