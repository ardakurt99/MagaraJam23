using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioSource[] sources;

    private void Start()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetFloat("Sound", .5f);
        }
        slider.value = PlayerPrefs.GetFloat("Sound");
        foreach (var item in sources)
        {
            item.volume = PlayerPrefs.GetFloat("Sound");
        }
    }
    public void SoundSettings()
    {
        PlayerPrefs.SetFloat("Sound", slider.value);

        foreach (var item in sources)
        {
            item.volume = PlayerPrefs.GetFloat("Sound");
        }

    }
}
