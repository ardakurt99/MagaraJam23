using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CatMove : MonoBehaviour
{

    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), Input.GetAxis("Vertical")) * speed *Time.deltaTime);
       
        

    }
}
