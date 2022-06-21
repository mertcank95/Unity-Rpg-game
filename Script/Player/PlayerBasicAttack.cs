using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttack : MonoBehaviour
{
    public float damage = 10f;
    private void OnTriggerEnter(Collider other)
    {
        EnemyHealt enemyHealth = other.GetComponent<EnemyHealt>();
        if(enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }


}
