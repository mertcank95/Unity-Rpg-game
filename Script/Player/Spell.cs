using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : PlayerSkillDamage
{
    public GameObject explosion;
    public float speed=10;
    void Start()
    {

        if (enemyLayer == (1 << LayerMask.NameToLayer("Enemy")))
        {
            //oyuncunun �n�n� g�sterme
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //playerin bakt��� y�n nereyse gidice�i y�n oras�
            transform.rotation = Quaternion.LookRotation(player.transform.forward);
        }
        else if (enemyLayer == (1 << LayerMask.NameToLayer("Player")))
        {
            //oyuncunun �n�n� g�sterme
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");
            //playerin bakt��� y�n nereyse gidice�i y�n oras�
            transform.rotation = Quaternion.LookRotation(boss.transform.forward);
        }
       
    }


    internal override void Update()
    {
        base.Update();//PlayerSkillDamage deki update burda �al���yor
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        if (colided)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }
    }


}
