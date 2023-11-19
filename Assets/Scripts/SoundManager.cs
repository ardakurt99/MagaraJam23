using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] sources;

    void Start()
    {
        sources = FindObjectsOfType<AudioSource>();
        foreach (var source in sources)
        {
            source.volume = PlayerPrefs.GetFloat("Sound");
        }
    }
}
