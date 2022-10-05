using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private AudioSource music;

    private void Awake()
    {
        music = GameObject.Find("Music").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (music)
        {
            volumeSlider.value = music.volume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChange);
        }
        else Debug.LogError("Music not found");
    }

    public void OnVolumeChange(float value)
    {
        music.volume = value;
    }
}
