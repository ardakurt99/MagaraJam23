using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


enum BossType { Boss1, Boss2, Boss3 }
public class Boss : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject character;
    [SerializeField] private Animator anim;


    [Header("Kedi Ayarları")]
    [SerializeField] private Transform catTransform;
    [SerializeField] private bool catFocus;

    [Header("Can ayarları")]
    [SerializeField] private float health;
    [SerializeField] private float firstHealth;

    [Header("Karekter ile alakalı ayarları")]
    [SerializeField] private float targetDistance;
    [SerializeField] private float findCharacterTime; // Kediye odaklandıktan sonra karakteri bulma süresi


    [Header("Saldırı Ayarları")]
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
        firstHealth = health;



        StartCoroutine("Fight");

        if (bossType == BossType.Boss1)
            StartCoroutine(CatControl());

    }
    void Update()
    {
        // Objenin hedefe doğru dönmesi

        if (catFocus && bossType == BossType.Boss1)
            transform.LookAt(catTransform);
        else
            transform.LookAt(character.transform);


        if (isAttack)
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
            agent.isStopped = false;
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

    internal void TakeDamge(float amount)
    {
        health -= amount;

        if (firstHealth / 2 >= health)
        {
            areaFightRatio = .7f;
        }

        if (health < 0)
        {
            health = 0;

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
    }

    public void BossPunch()
    {
        punchSound.Stop();
        punchSound.Play();
    }

    private IEnumerator Fight()
    {
        if (!catFocus)
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
