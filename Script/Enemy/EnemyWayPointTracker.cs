using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWayPointTracker : MonoBehaviour
{
    [Header("Haypoint")]
    public Transform[] walkpoint;
    [Header("Movement Settings")]
    public float trunSpeed = 5f;
    public float patrolTime = 10f;
    public float walkDistance = 8f;
    [Header("Attack Settings")]
    public float attackDistance = 1.4f;
    public float attackRate = 1f;

    Transform playerTarget;
    Animator anim;
    NavMeshAgent agent;
    float currentAttackTime;
    Vector3 nextDestination;
    int index;

    //Health
    EnemyHealt enemyHealt;

    public int Lenght { get; internal set; }

    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        index = Random.Range(0, walkpoint.Length);
        enemyHealt = GetComponent<EnemyHealt>();

        if (walkpoint.Length > 0)
        {
            InvokeRepeating("Patrol", Random.Range(0, patrolTime), patrolTime);
        }
    
    }
    void Start()
    {
       
        agent.avoidancePriority = Random.Range(1, 51);
    }

    
    void Update()
    {
        if (enemyHealt.currentHealt > 0)
        {
            MoveAndAttack();
        }
        else
        {
            anim.ResetTrigger("Hit");
            anim.SetBool("Death", true);
            agent.enabled = false;
            AudioManager.instance.PlaySfx(5);
            //anim.GetCurrentAnimatorStateInfo(0).normalizedTime>0.95f = animasyonun yüzde 95 tamamlanmýþsa
            if (!anim.IsInTransition(0)&&anim.GetCurrentAnimatorStateInfo(0).IsTag("Die")&&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
            {
                Destroy(gameObject, 5);
            }

        }
        
        
    }


    void MoveAndAttack()
    {
        float distance = Vector3.Distance(transform.position, playerTarget.position);
        if (distance > walkDistance)
        {
            //remainingDistance = kalan mesafe 
            //stoppingDistance = durulacak mesafe
            if (agent.remainingDistance >= agent.stoppingDistance)
            {
                agent.isStopped = false;
                agent.speed = 2f;
                anim.SetBool("Walk", true);

                nextDestination = walkpoint[index].position;
                agent.SetDestination(nextDestination);
                //SetDestination = en kýsa yoldan pozisyona ulaþýr

            }
            else//gitceðimiz konuma geldik
            {
                agent.isStopped = true;//artýk hareket etmiyecek
                agent.speed = 0;
                anim.SetBool("Walk", false);

                //yeni bir yer seçiyoruz bu sayeden enemy ile arasýndaki mesafeyi artýrýrýz
                //ve tekrar üsteki ife girer
                nextDestination = walkpoint[index].position;
                agent.SetDestination(nextDestination);
            }

        }
        else
        {
            if(distance > attackDistance + 0.15f && playerTarget.GetComponent<PlayerHealth>().currentHealth>0)
            {
                if(!anim.IsInTransition(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    anim.ResetTrigger("Attack")
                    agent.isStopped = false;
                    agent.speed = 3f;
                    anim.SetBool("Walk", true);
                    agent.SetDestination(playerTarget.position);
                }
                
            }
            else if (distance <= attackDistance && playerTarget.GetComponent<PlayerHealth>().currentHealth > 0)
            {
                agent.isStopped = true;
                anim.SetBool("Walk", false);
                agent.speed = 0;
                Vector3 targetPosition = new Vector3(playerTarget.position.x,
                    transform.position.y, playerTarget.position.z);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position),
                    trunSpeed * Time.deltaTime);
                if (currentAttackTime >= attackRate)
                {
                    anim.SetTrigger("Attack");
                    AudioManager.instance.PlaySfx(2);
                    currentAttackTime = 0;
                }
                else
                {
                    currentAttackTime += Time.deltaTime;
                }

            }
        }
        


    }



    void Patrol()
    {
        index = index == walkpoint.Length - 1? 0 : index+1;
        /*
         if(index==walkpoint.Length-1)
            index=0;
        else
            index+=1;
         
         */
    }



}
