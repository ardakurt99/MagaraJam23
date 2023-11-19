using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [Header("Ateş Verileri")]
    [SerializeField, Tooltip("Silahın Ateş etme uzaklığı")] private float range;
    [SerializeField] private Animator animator;


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


}
