using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private AudioSource pistolSound;
    [SerializeField] private float range;
    [SerializeField] private ParticleSystem fireBomb;
    [SerializeField] private CharacterControl characterScript;
    public void PistolSound()
    {
        pistolSound.Stop();
        pistolSound.Play();

        fireBomb.Stop();
        fireBomb.Clear();
        fireBomb.Play();
    }

    public void Shoot()
    {
        characterScript.ShootGun();
    }

    public void SceneReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
