using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Boss : MonoBehaviour
{

    Transform playerTarget;
    BossState bossStateChecker;
    NavMeshAgent agent;
    Animator anim;
    bool finishAttacking = true;

    public float trunSpeed;
    public float attackRate;
    float currentAttackTime;
    SphereCollider targetCollider;
    public static bool bossDeath = false;

    List<GameObject> allWayPointList = new List<GameObject>();

    [SerializeField] GameObject fireBall;
    [SerializeField] Transform  firePosition;

    private void Awake()
    {
        bossDeath = false;
        targetCollider = GetComponentInChildren<SphereCollider>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        bossStateChecker = GetComponent<BossState>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        allWayPointList.AddRange(GameObject.FindGameObjectsWithTag("WayPoint"));
    }
    void Update()
    {
        if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Scream"))
        {
            if(!AudioManager.instance.sfx[10].isPlaying)
            AudioManager.instance.PlaySfx(10);
        }
        if (finishAttacking)
        {
            GetControl();
        }
        else
        {
            anim.SetInteger("Attack", 0);
            if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                finishAttacking = true;
            }
        }

    }



    void GetControl()
    {
        if (bossStateChecker.state == BossState.State.DEATH)
        {
            agent.isStopped = true;
            anim.SetBool("Death", true);
            targetCollider.enabled = false;
            bossDeath = true;
            AudioManager.instance.PlaySfx(7);

        }
        else
        {
            if (bossStateChecker.state == BossState.State.CHASE)
            {
                agent.isStopped = false;
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
                anim.SetBool("WakeUp", true);
                agent.speed = 3f;
                agent.SetDestination(playerTarget.position);
            }
            else if(bossStateChecker.state == BossState.State.PATROL)
            {
                agent.isStopped = false;
                anim.ResetTrigger("Shoot");
                anim.SetBool("Run", false);
                anim.SetBool("Walk", true);
                anim.SetBool("WakeUp", true);

                if (agent.remainingDistance < 4f || !agent.hasPath)
                {
                    agent.speed = 2f;
                    PickRandomLocation();
                }

            }
            else if(bossStateChecker.state == BossState.State.SHOOT)
            {
                anim.SetBool("Run", false);
                anim.SetBool("Walk", false);
                anim.SetBool("WakeUp", true);
                LookPlayer();
                if(currentAttackTime >= attackRate)
                {
                    anim.SetTrigger("Shoot");
                    AudioManager.instance.PlaySfx(0);//0 indexli sesi çalýcak
                    Instantiate(fireBall, firePosition.position, Quaternion.identity);
                    currentAttackTime = 0;
                    finishAttacking = false;
                }
                else
                {
                    currentAttackTime += Time.deltaTime;
                }
            }
            else if(bossStateChecker.state == BossState.State.ATTACK)
            {
                anim.SetBool("Run", false);
                anim.SetBool("Walk", false);
                anim.SetBool("WakeUp", true);
                LookPlayer();

                if (currentAttackTime >= attackRate)
                {
                    int index = Random.Range(1, 3);
                    anim.SetInteger("Attack", index);
                    AudioManager.instance.PlaySfx(9);

                    currentAttackTime = 0f;
                    finishAttacking = false;
                }
                else
                {
                    currentAttackTime += Time.deltaTime;
                    anim.SetInteger("Attack", 0);
                }

            }
            else
            {
                anim.SetBool("WakeUp", false);
                anim.SetBool("Run", false);
                anim.SetBool("Walk", false);
                agent.isStopped = true;
            }


        }


    }


    void PickRandomLocation()
    {
        GameObject pos = GetRandomPoint();
        agent.SetDestination(pos.transform.position);
    }



    GameObject GetRandomPoint()
    {
        int index = Random.Range(0, allWayPointList.Count);
        return allWayPointList[index];
    }

    void LookPlayer()
    {
        //karakterin playera dönemsi
        Vector3 targetPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position),
            trunSpeed*Time.deltaTime);


    }



}
