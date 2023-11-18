using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum BeginMode { FirstText, Bell, ShowTheDoor, SeeComic, WillHoldComic, HoldComic }
public class CharacterHomeControl : MonoBehaviour
{
    [SerializeField, TextArea(3, 10)] private List<string> beginText;
    [SerializeField] private List<ComicBook> comicPages;
    [SerializeField] private TextMeshProUGUI speachText;
    [SerializeField] private Image spachBg;
    [SerializeField] private Image comicPage;
    [SerializeField] private BeginMode beginMode;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject hasAnimatorObject;
    [SerializeField] private GameObject comicBook;
    [SerializeField] private GameObject comicPage3D;
    [SerializeField] private Transform readTransform;
    private int beginTextIndex = 0;
    private int comicTextIndex = 0;

    [Header("Kapı Ayarları")]
    [SerializeField] private AudioSource doorBell;
    [SerializeField] private Animator doorAnim;
    [SerializeField] private Transform doorTransform;
    [SerializeField] private float doorDistance;
    private bool doorIsOpen = false;
    [SerializeField] private bool doorCanOpen = false;


    private void Start()
    {

        if (!PlayerPrefs.HasKey("Episode"))
        {
            PlayerPrefs.SetInt("Episode", 0);
        }

        if (PlayerPrefs.GetInt("Episode") == 0)
        {
            beginMode = BeginMode.FirstText;
        }

        if (beginMode == BeginMode.FirstText)
        {
            StartCoroutine(ShowBeginText(5));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && doorCanOpen && !doorIsOpen)
        {
            doorAnim.SetTrigger("Open");
            doorIsOpen = true;
            speachText.text = "";
            spachBg.color = new Color32(0, 0, 0, 0);
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            animator.SetBool("Read", true);

            hasAnimatorObject.transform.position = readTransform.position;
            hasAnimatorObject.transform.rotation = readTransform.rotation;
        }

        if(beginMode == BeginMode.WillHoldComic && !Input.GetKeyDown(KeyCode.E))
        {
            speachText.text = "Çizgi romanı tutmak için E tuşuna basın";
            spachBg.color = new Color32(0, 0, 0, 220);
        }
        else if (beginMode == BeginMode.WillHoldComic && Input.GetKeyDown(KeyCode.E))
        {
            speachText.text = "";
            spachBg.color = new Color32(0, 0, 0, 0);

            beginMode = BeginMode.HoldComic;
            comicBook.SetActive(false);

            animator.SetBool("Read", true);
            hasAnimatorObject.SetActive(true);
            comicPage3D.SetActive(true);
            hasAnimatorObject.transform.position = readTransform.position;
            hasAnimatorObject.transform.rotation = readTransform.rotation;
        }
    }

    private void BellMode()
    {
        doorBell.Play();

        StartCoroutine(ShowBeginText(5));

        beginMode = BeginMode.ShowTheDoor;
        StartCoroutine(DoorControl());
    }

    IEnumerator ShowBeginText(int time)
    {
        yield return new WaitForSeconds(1);

        speachText.text = beginText[beginTextIndex];
        spachBg.color = new Color32(0, 0, 0, 220);

        beginTextIndex++;
        yield return new WaitForSeconds(time);

        if (beginTextIndex == 1)
            BellMode();

        if (beginTextIndex == 2)
        {
            yield return new WaitForSeconds(1.5f);
            for (int i = 2; i < beginText.Count; i++)
            {
                speachText.text = beginText[i];
                spachBg.color = new Color32(0, 0, 0, 220);

                yield return new WaitForSeconds(time);

                if(beginText.Count -1 == i)
                {
                    beginMode = BeginMode.WillHoldComic;
                }
            }
        }
    }

    IEnumerator DoorControl()
    {
        yield return new WaitForSeconds(3f);
        while (!doorIsOpen)
        {
            if (Vector3.Distance(doorTransform.position, transform.position) <= doorDistance)
            {
                doorCanOpen = true;
                speachText.text = "Kapıyı açmak için E tuşuna basın";
                spachBg.color = new Color32(0, 0, 0, 220);
            }
            else
            {
                doorCanOpen = false;
                speachText.text = "Kapıya Gidin";
                spachBg.color = new Color32(0, 0, 0, 220);
            }
            yield return new WaitForSeconds(.5f);
        }
    }
}
