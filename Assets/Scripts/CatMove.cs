using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows.Speech;

public enum CatMode { Boss, Standart, Job }

public class CatMove : MonoBehaviour
{

    [SerializeField] private CatMode catMode;
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float maxTargetDistance;
    [SerializeField] private float minBossDistance;

    NavMeshAgent nav;
    [SerializeField]Animator animator;
    

    [SerializeField] List<Transform> hedefler;
    [SerializeField] Transform hedef = null;


    [SerializeField] Transform boss;
    [SerializeField] GameObject bossTag;
    [SerializeField] Transform adam;
    [SerializeField] GameObject adamTag;

    [SerializeField] GameObject[] gameObjects;
    [SerializeField] bool bossEtki;


    // Start is called before the first frame update
    void Start()
    {
        speed = 10;

        nav = this.gameObject.GetComponent<NavMeshAgent>();
        

    }

    // Update is called once per frame

    void Update()
    {


        if (catMode == CatMode.Job)
        {
            transform.Translate(new Vector3(0, Input.GetAxis("Jump"), Input.GetAxis("Vertical")) * speed * Time.deltaTime);
            animator.SetBool("IsWalk", true);

            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * speed * 10 * Time.deltaTime);

            if (hedef == null)
            {

                for (int i = 0; i < hedefler.Count; i++)
                {
                    if (Vector3.Distance(transform.position, hedefler[i].position) < maxTargetDistance)
                    {
                        nav.isStopped = false;
                        hedef = hedefler[i];
                        break;
                    }
                }
            }
            else
            {
                nav.SetDestination(hedef.position);

                animator.SetBool("IsWalk", true);

            }



            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                hedefler.Remove(hedef);
                hedef = null;
                nav.isStopped = true;


            }


        }

        if(catMode == CatMode.Boss)
        {

            animator.SetBool("IsWalk", true);
            if(!bossEtki)
            {
                nav.SetDestination(boss.position);
                
                
                

            }
            else
            {

                nav.SetDestination(adam.position);


            }
        }
    }

 private void OnCollisionEnter(Collision other) {
    if(other.collider.CompareTag("Boss"))
    {
        bossEtki = true;
    }

    if(other.collider.CompareTag("Adam"))
    {
       bossEtki = false; 
    }
}

}
