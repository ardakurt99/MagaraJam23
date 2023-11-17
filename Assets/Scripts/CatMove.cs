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

    NavMeshAgent nav;

    public Transform hedef, hedef2, hedef3;

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

       nav.SetDestination(hedef.position);
       nav.SetDestination(hedef2.position);
       nav.SetDestination(hedef3.position);

    }

}
