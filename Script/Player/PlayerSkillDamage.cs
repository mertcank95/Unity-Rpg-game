using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDamage : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float radius = 0.5f;
    public float damageCount=10;
    EnemyHealt enemyHealt;
    protected bool colided;//bir kere hasar vermek için
    PlayerHealth playerHealt;
    //internal : miras classdan eriþilir
    
    internal virtual void Update()
    {
        // transform.position da radius deðiþkeninin yarý çapý kadar bir küre yarat ve bu enemyLayerý olan bir obje içine girerse 
        //colliderin içerisine at
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach (Collider hit in hits)//isabet eten tüm enemyler
        {
            //LayerMask 32 bitden oluþur (0000..1)
            // 1 layerin numarasý
            //LayerMask.NameToLayer("Enemy") = 12 ci layerda
            //<< = 1 rakamaný al 11 birim kadar git ve biri yerleþtir
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
