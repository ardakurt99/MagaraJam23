using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// Şimdi Yazıları yazma gibi eventleri yapacaksın. Bunun için Enum kullan
enum HomeModes { TextShow, Comic }
public class HomeManager : MonoBehaviour
{

    [SerializeField, TextArea(3, 10)] private List<string> beginText;
    [SerializeField] private List<ComicBook> comicPages;
    [SerializeField] private TextMeshProUGUI speachText;
    [SerializeField] private Image comicPage;
    [SerializeField] private AudioSource doorBell;

    [SerializeField] private HomeModes homeMode;

    private int beginTextIndex = 0;
    private int comicTextIndex = 0;

    private void Start()
    {
        
        if(!PlayerPrefs.HasKey("Episode"))
        {
            PlayerPrefs.SetInt("Episode", 0);
        }

        if(PlayerPrefs.GetInt("Episode") == 0)
        {
            homeMode = HomeModes.TextShow;
        }
        else
        {
            homeMode = HomeModes.Comic;
        }

        if(homeMode == HomeModes.TextShow)
        {
            
        }
        StartCoroutine("ShowBeginText");
    }

    IEnumerator ShowBeginText()
    {
        yield return new WaitForSeconds(1);

        for (int i = 0; i < beginText.Count; i++)
        {
            if(i == 1)
                doorBell.Play();

            speachText.text = beginText[i];

            Debug.Log(beginText[i].Length / 10);

            yield return new WaitForSeconds(beginText[i].Length / 10);
        }
    }
    
}
