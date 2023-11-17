using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class CatMove : MonoBehaviour
{

    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float maxTargetDistance;

    NavMeshAgent nav;

    public List<Transform> hedefler;
    public Transform hedef = null;

    public Transform adam;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10;

        nav = this.gameObject.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), Input.GetAxis("Vertical")) * speed *Time.deltaTime);

    //    nav.SetDestination(hedef.position);
    //    nav.SetDestination(hedef2.position);
    //    nav.SetDestination(hedef3.position);


        if(hedef == null)
        {
            
        for (int i = 0; i < hedefler.Count; i++)
        {
            if(Vector3.Distance(transform.position, hedefler[i].position) < maxTargetDistance)
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
        


        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            hedefler.Remove(hedef);
            hedef = null;
            nav.isStopped = true;
            

            
        }
    }

}
