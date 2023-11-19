using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioSource[] sources;
    public void SoundSettings()
    {
        PlayerPrefs.SetFloat("Sound", slider.value);

        foreach (var item in sources)
        {
            item.volume = PlayerPrefs.GetFloat("Sound");
        }

    }
}
