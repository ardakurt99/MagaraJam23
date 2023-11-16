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

    [SerializeField] private float targetMaxDistance; 
    [SerializeField] private List<Target> targets;
    [SerializeField] private Target target;

    [SerializeField] private bool lockedOnTarget;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        StartCoroutine(TargetControl());

    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), Input.GetAxis("Vertical")) * speed *Time.deltaTime);

       if(lockedOnTarget)
       {
        transform.position = Vector3.Lerp((target.transform.position) - Vector3.one * 3, transform.position, .8f);
       }
       
       if(Input.GetKeyDown(KeyCode.Backspace))
       {
        lockedOnTarget = false;
        target.IsActive =false;
       }
    }

    IEnumerator TargetControl()
    {
        while(true)
        {
            if(!lockedOnTarget)
            {
                for (int i = 0; i < targets.Count; i++)
                {
                    if(targets[i].IsActive && Vector3.Distance(targets[i].gameObject.transform.position, transform.position) < targetMaxDistance)
                    {
                        lockedOnTarget = true;
                        target = targets[i];
                        break;
                    }
                }

               
            }

             yield return new WaitForSeconds(.5f);
        }
    }
}
