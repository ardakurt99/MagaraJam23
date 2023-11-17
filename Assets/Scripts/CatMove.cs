using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.AI;

public enum CatMode { Boss, Standart, Job }

public class CatMove : MonoBehaviour
{

    [SerializeField] private CatMode catMode;
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float maxTargetDistance;

    NavMeshAgent nav;
    [SerializeField]Animator animator;
    

    public List<Transform> hedefler;
    public Transform hedef = null;

    public Transform boss;
    public Transform adam;


    public bool bossEtki;

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
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), Input.GetAxis("Vertical")) * speed * Time.deltaTime);


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
            nav.SetDestination(boss.position);
            animator.SetBool("IsWalk", true);

                if (Vector3.Distance(transform.position, boss.position) < maxTargetDistance)
                {
                    nav.isStopped = false;
                }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                boss = adam;
                nav.SetDestination(adam.position);


            }
        }
    }

}
