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
            //oyuncunun önünü gösterme
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //playerin baktýðý yön nereyse gidiceði yön orasý
            transform.rotation = Quaternion.LookRotation(player.transform.forward);
        }
        else if (enemyLayer == (1 << LayerMask.NameToLayer("Player")))
        {
            //oyuncunun önünü gösterme
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");
            //playerin baktýðý yön nereyse gidiceði yön orasý
            transform.rotation = Quaternion.LookRotation(boss.transform.forward);
        }
       
    }


    internal override void Update()
    {
        base.Update();//PlayerSkillDamage deki update burda çalýþýyor
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        if (colided)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }
    }


}
