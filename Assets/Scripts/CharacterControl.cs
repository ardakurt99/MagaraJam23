using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [Header("Ateş Verileri")]
    [SerializeField, Tooltip("Silahın Ateş etme uzaklığı")] private float range;
    [SerializeField] private Animator animator;

    void Start()
    {

    }

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
        RaycastHit hit;
        animator.SetTrigger("Shoot");
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {

            if(hit.collider.CompareTag("Boss"))
            {
                Debug.Log("10 Hasar");
            }
            else if(hit.collider.CompareTag("Back"))
            {
                Debug.Log("20 Hasar");
            }

        }
    }
}
