using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
enum BeginMode { Museum, FirstText, Bell, ShowTheDoor, SeeComic, WillHoldComic, HoldComic, SceneTransition }
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

    [SerializeField] private PostProcessVolume postProcessVolume;
    [SerializeField] private Vignette vignette;
    private int beginTextIndex = 0;
    private int comicTextIndex = 0;

    [Header("Kapı Ayarları")]
    [SerializeField] private AudioSource doorBell;
    [SerializeField] private Animator doorAnim;
    [SerializeField] private Transform doorTransform;
    [SerializeField] private float doorDistance;
    private bool doorIsOpen = false;
    [SerializeField] private bool doorCanOpen = false;
    [SerializeField] private AudioSource doorOpenSound;

    private Camera camera;

    private void Start()
    {
        camera = Camera.main.GetComponent<Camera>();
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
            //StartCoroutine(ShowBeginText(5));
            StartCoroutine(ShowBeginText(3));
        }
    }

    private void Update()
    {
        switch (beginMode)
        {
            case BeginMode.WillHoldComic:
                if (!Input.GetKeyDown(KeyCode.E))
                {
                    speachText.text = "Çizgi romanı tutmak için E tuşuna basın";
                    spachBg.color = new Color32(0, 0, 0, 220);
                }
                else if (Input.GetKeyDown(KeyCode.E))
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
                break;
            case BeginMode.HoldComic:
                if (!Input.GetKeyDown(KeyCode.E))
                {
                    if (camera.fieldOfView > 30)
                    {
                        camera.fieldOfView -= Time.deltaTime * 30;
                    }
                    else
                    {
                        camera.fieldOfView = 30;
                    }

                    speachText.text = "Okuduğunuzda E tuşuna basın";
                    spachBg.color = new Color32(0, 0, 0, 220);
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    beginMode = BeginMode.SceneTransition;

                    StartCoroutine(SceneTransition());


                }
                break;
        }
        if (Input.GetKeyDown(KeyCode.E) && doorCanOpen && !doorIsOpen)
        {
            doorAnim.SetTrigger("Open");
            doorOpenSound.Stop();
            doorOpenSound.Play();
            doorIsOpen = true;
            speachText.text = "";
            spachBg.color = new Color32(0, 0, 0, 0);
            StartCoroutine(ShowBeginText(3));
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

        if (beginTextIndex == 3)
        {
            yield return new WaitForSeconds(1.5f);
            for (int i = 2; i < beginText.Count; i++)
            {
                speachText.text = beginText[i];
                spachBg.color = new Color32(0, 0, 0, 220);

                yield return new WaitForSeconds(time);

                if (beginText.Count - 1 == i)
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

    private IEnumerator SceneTransition()
    {
        Debug.Log("Buraya Girdi");
        yield return new WaitForSeconds(.5f);

        while (postProcessVolume.weight < 1)
        {
            postProcessVolume.weight += .05f;
            yield return new WaitForSeconds(.05f);
        }
        yield return new WaitForSeconds(.15f);
        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            while (vignette.intensity.value < 1)
            {

                vignette.intensity.value += .05f;
                yield return new WaitForSeconds(.05f);
            }
        }


        speachText.text = "Burdan sonra sahne geçecek";
        spachBg.color = new Color32(0, 0, 0, 220);
    }
}
