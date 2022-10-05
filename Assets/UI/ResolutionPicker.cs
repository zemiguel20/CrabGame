using UnityEngine;
using TMPro;
using System;

public class ResolutionPicker : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    private void Start()
    {
        // Determine default option based on current based resolution
        string res = Screen.width.ToString() + "x" + Screen.height.ToString();
        int option = dropdown.options.FindIndex(opt => opt.text == res);
        if (option >= 0)
            dropdown.value = option;
        else
            dropdown.value = 0;

        dropdown.onValueChanged.AddListener(OnResolutionChange);
    }

    public void OnResolutionChange(int option)
    {
        string[] res = dropdown.options[option].text.Split("x");
        int width = Int32.Parse(res[0]);
        int height = Int32.Parse(res[1]);
        Debug.Log(width);
        Debug.Log(height);
        Screen.SetResolution(width, height, false);
    }
}
