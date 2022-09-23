using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        // Set resolution to square

        int length =
            Display.main.systemWidth > Display.main.systemHeight ?
            Display.main.systemHeight :
            Display.main.renderingWidth;

        Screen.SetResolution(length, length, true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
