using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpsScript;
    [SerializeField] private Boss bossScript;

    [Header("Ateş Verileri")]
    [SerializeField, Tooltip("Silahın Ateş etme uzaklığı")] private float range;
    [SerializeField] private Animator animator;
    public float maxHealth = 200;
    public float health = 200;



    [Header("UI Ayarları")]
    [SerializeField] private Image characterHealth;
    [SerializeField] private Image bossHealth;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        


        Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position + (Camera.main.transform.forward * range), Color.black);
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");
    }

    public void TakeDamage(float value)
    {
        health -= value;

        if(health < 0 )
        {
            health = 0;
            fpsScript.Die();
        }
        characterHealth.fillAmount = Mathf.Clamp01(health / maxHealth); 
    }


    public void ShootGun()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {

            if (hit.collider.CompareTag("Boss"))
            {
                bossScript.TakeDamage(10);
                bossHealth.fillAmount = Mathf.Clamp01(bossScript.Health / bossScript.FirstHealth);


            }
            else if (hit.collider.CompareTag("Back"))
            {
                bossScript.TakeDamage(20);
                bossHealth.fillAmount = Mathf.Clamp01(bossScript.Health / bossScript.FirstHealth);
            }

        }
    }




}
