using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : MonoBehaviour
{
    public enum State//deðer saðlanýyorsa bunu yap
    {
        NONE,
        SLEEP,
        PATROL,
        CHASE,
        ATTACK,
        SHOOT,
        DEATH
    }

    Transform playerTarget;
    State bossState = State.SLEEP;
    public State state { get { return bossState; } }

    float distanceToTarget;
    EnemyHealt enemyHealt;

    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        enemyHealt = GetComponent<EnemyHealt>();
        bossState = State.SLEEP;
    }

    private void Update()
    {
        SetState();
    }

    void SetState()
    {
        distanceToTarget = Vector3.Distance(transform.position, playerTarget.position);

        if(bossState == State.SLEEP)
        {
            int enemyCount = FindObjectsOfType<EnemyWayPointTracker>().Length;
            

            if(enemyHealt.currentHealt < enemyHealt.maxHealth)
            {
                bossState = State.NONE;
            }
            else if(distanceToTarget <= 4f)
            {
                bossState = State.NONE;
            }
            else if (enemyCount <= 0)
            {
                bossState = State.NONE;
            }
            else
            {
                bossState = State.SLEEP;

            }



        }
        else if(bossState != State.DEATH || bossState != State.SLEEP)
        {
            if (distanceToTarget > 4f && distanceToTarget <= 8f)
            {
                bossState = State.CHASE;
            }
            else if (distanceToTarget > 8f && distanceToTarget <= 12f)
            {
                bossState = State.SHOOT;
            }
            else if (distanceToTarget > 12f)
            {
                bossState = State.PATROL;
            }
            else if(distanceToTarget <= 4f)
            {
                bossState = State.ATTACK;
            }
            else
            {
                bossState = State.NONE;
            }

        }

        if (enemyHealt.currentHealt <= 0f)
        {
            bossState = State.DEATH;
        }




    }


}
