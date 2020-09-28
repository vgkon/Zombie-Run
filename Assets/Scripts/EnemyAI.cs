using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[ExecuteInEditMode]
public class EnemyAI : MonoBehaviour
{
    Transform target;
    [SerializeField] float chaseRange = 15f;
    [SerializeField] float turnSpeed = 5f;
    EnemyHealth enemyHealth;
    [SerializeField] AudioClip[] zombieSoundsIdle;
    [SerializeField] AudioClip zombieSoundChase;
    [SerializeField] AudioClip zombieSoundAttack;
    [SerializeField] AudioSource audioSource;
    bool isDead = false;
    bool isClose = false;

    bool isProvoked = false;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        audioSource.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.IsDead())
        {
            enabled = false;
            audioSource.enabled = false;
            navMeshAgent.enabled = false;
            return;
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget < chaseRange)
        {
            isProvoked = true;
        }

        
    }

    public void IdleSounds()
    {
        if (distanceToTarget < 25f && distanceToTarget > chaseRange)
        {
            print(gameObject.name + " close to target");
            if (!audioSource.isPlaying)
            {
                print(gameObject.name + " AudioScource is not currently playing");
                if (Random.Range(0.0f, 1.0f) > 0.8f)
                {
                    print(gameObject.name + " is speaking");
                    audioSource.PlayOneShot(zombieSoundsIdle[Random.Range(0, zombieSoundsIdle.Length - 1)]);
                }
                else
                {
                    print(gameObject.name + " didn't speak this time");
                } 
            }
        }
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget > navMeshAgent.stoppingDistance)
        {
            GetComponent<Animator>().SetBool("attack", false);
            ChaseTarget();
        }
        else
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
    }

    public void AttackSounds()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(zombieSoundAttack);
        }

    }
    private void ChaseTarget()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(zombieSoundChase);
        }
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(100, 0, 0, 1f);
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    private void FaceTarget()
    {
        Vector3 direction = ((target.position - transform.position).normalized);
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }
}