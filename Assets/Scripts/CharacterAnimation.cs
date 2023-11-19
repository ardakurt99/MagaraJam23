using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private AudioSource pistolSound;
    [SerializeField] private float range;
    [SerializeField] private ParticleSystem fireBomb;
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
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {

            if (hit.collider.CompareTag("Boss"))
            {
                Debug.Log("10 Hasar");
            }
            else if (hit.collider.CompareTag("Back"))
            {
                Debug.Log("20 Hasar");
            }

        }
    }
}
