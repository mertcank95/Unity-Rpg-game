using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDamage : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float radius = 0.5f;
    public float damageCount=10;
    EnemyHealt enemyHealt;
    protected bool colided;//bir kere hasar vermek i�in
    PlayerHealth playerHealt;
    //internal : miras classdan eri�ilir
    
    internal virtual void Update()
    {
        // transform.position da radius de�i�keninin yar� �ap� kadar bir k�re yarat ve bu enemyLayer� olan bir obje i�ine girerse 
        //colliderin i�erisine at
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach (Collider hit in hits)//isabet eten t�m enemyler
        {
            //LayerMask 32 bitden olu�ur (0000..1)
            // 1 layerin numaras�
            //LayerMask.NameToLayer("Enemy") = 12 ci layerda
            //<< = 1 rakaman� al 11 birim kadar git ve biri yerle�tir
            //enemyLayer = 1 << 11
            if (enemyLayer == (1 << LayerMask.NameToLayer("Enemy")))
            {
                enemyHealt = hit.gameObject.GetComponent<EnemyHealt>();
                colided = true;
            }
            else if(enemyLayer == (1 << LayerMask.NameToLayer("Player")))
            {
                playerHealt = hit.gameObject.GetComponent<PlayerHealth>();
                colided = true;
            }
           
            
            if (colided)
            {
               

                if (enemyLayer == (1 << LayerMask.NameToLayer("Enemy")))
                {
                    if (enemyHealt != null)
                    {
                        enemyHealt.TakeDamage(damageCount);
                        enabled = false;
                    }
                    
                }
                else if (enemyLayer == (1 << LayerMask.NameToLayer("Player")))
                {
                    if (playerHealt != null)
                    {
                        playerHealt.TakeDamage(damageCount);
                        enabled = false;
                    }
                }

              
            }
        }


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,radius);
    }


}
