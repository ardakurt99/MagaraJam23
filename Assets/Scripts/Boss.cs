using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

enum BossType { Boss1, Boss2, Boss3 }
public class Boss : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject character;
    [SerializeField] private Animator anim;
    [SerializeField] private LevelLoader screenLoader;


    [Header("Kedi Ayarları")]
    [SerializeField] private Transform catTransform;
    [SerializeField] private bool catFocus;

    [Header("Can ayarları")]
    public float Health;
    public float FirstHealth;

    [Header("Karekter ile alakalı ayarları")]
    [SerializeField] private float targetDistance;
    [SerializeField] private float findCharacterTime; // Kediye odaklandıktan sonra karakteri bulma süresi


    [Header("Saldırı Ayarları")]
    [SerializeField] private bool isActive;
    [SerializeField] private float areaDamageDistance;
    [SerializeField] private float punchDamageDistance;
    [SerializeField] private float areaFightRatio;
    [SerializeField] private float minAttackTime;
    [SerializeField] private float maxAttackTime;
    [SerializeField] private bool isAttack;
    [SerializeField] private bool isAreaAttack;
    [SerializeField] private float areaDamageDamageAmount;
    [SerializeField] private float punchDamageDamageAmount;


    [Header("Sesler")]
    [SerializeField] private AudioSource stepSound;
    [SerializeField] private AudioSource punchSound;
    [SerializeField] private AudioSource jumpSound;

    [Header("Boss Tipi")]
    [SerializeField] private BossType bossType;

    private void Start()
    {
        FirstHealth = Health;


        if(isActive)
        StartCoroutine("Fight");

        if (bossType == BossType.Boss1)
            StartCoroutine(CatControl());
    }
    void Update()
    {
        if(Health <= 0)
        {
            agent.isStopped = true;
            return;
        }
        // Objenin hedefe doğru dönmesi

        if (catFocus && bossType == BossType.Boss1)
            transform.LookAt(new Vector3(catTransform.transform.position.x, transform.position.y, catTransform.transform.position.z));
        else
            transform.LookAt(new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z));


        if (isAttack && isActive)
        {
            Attack();
        }
    }



    private void Attack()
    {
        anim.SetBool("Walk", true);

        if (catFocus)
        {
            agent.isStopped = true;
            return;
        }
        else
        {
            //agent.isStopped = false;
            agent.SetDestination(character.transform.position);
        }


        //Debug.Log($"{agent.remainingDistance } - {areaDamageDistance}");



        if (isAreaAttack)
        {
            if (!agent.pathPending && agent.remainingDistance < areaDamageDistance)
            {
                agent.isStopped = true;
                anim.SetTrigger("Jump");
                //anim.SetBool("Walk", false);
                isAttack = false;
                StartCoroutine("Fight");

            }
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < punchDamageDistance)
            {
                agent.isStopped = true;
                anim.SetTrigger("Punch");
                anim.SetBool("Walk", false);
                isAttack = false;
                StartCoroutine("Fight");
            }
        }
    }

    public void ChangeCatFocus()
    {
        catFocus = false;
        StartCoroutine(CatControl());
    }

    internal void ChangeActive(bool active)
    {
        isActive = active;



        if (isActive)
            StartCoroutine("Fight");

        if (bossType == BossType.Boss1)
            StartCoroutine(CatControl());
    }

    internal void TakeDamage(float amount)
    {
        Health -= amount;

        Debug.Log(Health);

        if (FirstHealth / 2 >= Health)
        {
            areaFightRatio = .7f;
        }

        if (Health <= 0)
        {
            Health = 0;

            anim.SetBool("Die", true);
        }

        
    }

    public void BossStep()
    {
        stepSound.Stop();
        stepSound.Play();
    }

    public void BossJump()
    {
        jumpSound.Stop();
        jumpSound.Play();

        if (Vector3.Distance(new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z), transform.position) <= areaDamageDistance)
        {
            character.GetComponent<CharacterControl>().TakeDamage(areaDamageDamageAmount);
        }
    }

    public void BossPunch()
    {
        punchSound.Stop();
        punchSound.Play();

        if(Vector3.Distance(character.transform.position, transform.position) <= punchDamageDistance)
        {
            character.GetComponent<CharacterControl>().TakeDamage(punchDamageDamageAmount);
        }
    }

    public void NextScene()
    {
        screenLoader.LoadLevel();
    }

    private IEnumerator Fight()
    {
        if (!catFocus && isActive && Health > 0)
        {
            agent.isStopped = false;

            yield return new WaitForSeconds(Random.Range(minAttackTime, maxAttackTime));

            isAreaAttack = areaFightRatio > Random.Range((float)0, (float)1);

            isAttack = true;
        }

    }

    private IEnumerator CatControl()
    {
        while (!catFocus)
        {
            yield return new WaitForSeconds(5);
            if (Vector3.Distance(transform.position, catTransform.position) <= 5)
            {
                catFocus = true;
                anim.SetTrigger("Defeat");
            }
        }


    }
}
