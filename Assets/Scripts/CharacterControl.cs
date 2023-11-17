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
            
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddForce(Vector3.forward * 5, ForceMode.Impulse);
            }
            
        }
    }
}
