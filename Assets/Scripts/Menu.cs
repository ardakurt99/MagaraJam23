using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private AudioSource clickFx;
    public void PlaySound()
    {
        clickFx.Stop();
        clickFx.Play();
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void Basla()
    {
        SceneManager.LoadScene("Home");
    }
    
    public void Devam()
    {
    
    }

    public void Ayarlar()
    {
        //SceneManager.LoadScene("Ayarlar");
    }

    public void Yapimcilar()
    {
        //SceneManager.LoadScene("Yapimcilar");
    }
}
