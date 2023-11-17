using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComicBookManager : MonoBehaviour
{
    [SerializeField] private List<ComicBook> comicPages;

    [SerializeField] private TextMeshProUGUI comicText;
    [SerializeField] private Image comicPage;
    private List<string> comicPageTexts;
    private int comicTextQueue = 0;
    private void Start()
    {
        comicPage.sprite = comicPages[PlayerPrefs.GetInt("Episode")].ComicPage;
        comicPageTexts = comicPages[PlayerPrefs.GetInt("Episode")].ComicTexts;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeComicText();
        }
    }

    private void ChangeComicText()
    {
        comicText.text = comicPageTexts[comicTextQueue];
        comicTextQueue++;
    }
}