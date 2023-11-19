using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public enum CatMode {Follow, Job, Boss }

public class CatMove : MonoBehaviour
{
    [SerializeField] private GameObject bossPosition;
    [SerializeField] NavMeshAgent agent;

    [SerializeField] private Animator anim;

    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject character;

    [SerializeField] CatMode catMode;

    [SerializeField] private FirstPersonController fpsScript;
    [SerializeField] private GameObject door;

    private void Update()
    {
        switch (catMode)
        {
            case CatMode.Follow:

                transform.LookAt(new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z));

                if (Vector3.Distance(new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z), transform.position) < 6.5f)
                {
                    anim.SetBool("IsWalk", false);
                }
                else
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * 3);
                    anim.SetBool("IsWalk", true);
                }
                break;
            case CatMode.Job:

                transform.Translate(new Vector3(0, 0, Input.GetAxis("Vertical")) * 5 * Time.deltaTime);
                anim.SetBool("IsWalk", true);

                transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * 5 * 10 * Time.deltaTime);


                //nav.SetDestination(hedef.position);

                

                anim.SetBool("IsWalk", !(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0));

                break;


            case CatMode.Boss:

                transform.LookAt(new Vector3(bossPosition.transform.position.x, transform.position.y, bossPosition.transform.position.z));

                transform.Translate(Vector3.forward * Time.deltaTime * 3);
                break;
        }
    }

    public void ChaangeMode(CatMode mode)
    {
        catMode = mode;
    }

    public CatMode ReadCatMode() => catMode;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cikis"))
        {
            catMode = CatMode.Boss;

            fpsScript.IsMove = true;
            fpsScript.GlassAnim.SetTrigger("Close");
            door.SetActive(false);
        }
    }
}
