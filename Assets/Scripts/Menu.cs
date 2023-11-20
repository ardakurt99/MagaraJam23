using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private AudioSource clickFx;
    [SerializeField] private GameObject ayarlar;
    [SerializeField] private GameObject yapimcilar;
    [SerializeField] private GameObject anaMenu;
    
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
    

    public void Ayarlar()
    {
        ayarlar.SetActive(true);
        yapimcilar.SetActive(false);
        anaMenu.SetActive(false);
    }

    public void Yapimcilar()
    {
        yapimcilar.SetActive(true);

        ayarlar.SetActive(false);
        anaMenu.SetActive(false);
    }

    public void AnaMenu()
    {
        anaMenu.SetActive(true);

        ayarlar.SetActive(false);
        yapimcilar.SetActive(false);
    }
}
