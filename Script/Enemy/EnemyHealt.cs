using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHealt : MonoBehaviour
{

    [HideInInspector] public float currentHealt;
    public float maxHealth = 100f;
    Animator anim;
    [SerializeField] Image enemyHealtBar;
     SphereCollider targetCollider;

    public int expAmount = 10;
    public static event Action<int> onDeath;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        targetCollider = GetComponentInChildren<SphereCollider>();

        //random can atama
        if (this.gameObject.tag == "Boss")
        {
            maxHealth = UnityEngine.Random.Range(50, 75);
        }
        else if (this.gameObject.tag == "Enemy")
        {
            maxHealth = UnityEngine.Random.Range(25, 50);


        }
        currentHealt = maxHealth;

    }

    public void TakeDamage(float amount)
    {
        currentHealt -= amount;
        enemyHealtBar.fillAmount = currentHealt / maxHealth;
        if (currentHealt > 0)
        {
            if(this.gameObject.tag == "Boss")
            {
                AudioManager.instance.PlaySfx(6);
            }
            else if(this.gameObject.tag == "Enemy")
            {
                AudioManager.instance.PlaySfx(3);

            }
            anim.SetTrigger("Hit");
        }
        if (currentHealt <= 0)
        {
            
           Canvas canvas= enemyHealtBar.gameObject.GetComponentInParent<Canvas>();
            onDeath(expAmount);//öldüðümüz zaman event tetiklencek
            if(targetCollider.gameObject.activeInHierarchy)
            targetCollider.gameObject.SetActive(false);

            if(canvas.gameObject.activeInHierarchy)
                canvas.gameObject.SetActive(false);

        }


    }

    





}
