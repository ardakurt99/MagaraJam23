using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComicBook
{
    [SerializeField] internal Sprite ComicPage;

    [SerializeField, TextArea(3, 10)] internal List<string> ComicTexts;

}
