using UnityEngine;

public class KeepAspectRatio : MonoBehaviour
{
    private void Awake()
    {

        int length =
            Display.main.systemWidth > Display.main.systemHeight ?
            Display.main.systemHeight :
            Display.main.renderingWidth;

        Screen.SetResolution(length, length, true);
    }
}
