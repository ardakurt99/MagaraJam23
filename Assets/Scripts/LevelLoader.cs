using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Slider loadSlider;
    [SerializeField] private GameObject loadingScreen;

    public void LoadLevel ()
    {
        StartCoroutine(LoadScene(1));
    }

    System.Collections.IEnumerator LoadScene(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadSlider.value = progress;
            Debug.Log(progress);
            yield return null;
        }
    }
}
