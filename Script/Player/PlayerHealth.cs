using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [HideInInspector] public float currentHealth;
    public float maxHealth=100f;
    Image healthBar;
    bool isShielded;
    public bool Shielded { get { return isShielded; } set { isShielded = value; } }
    Animator anim;
    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar = GameObject.Find("HealtOrb").GetComponent<Image>();
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float amount)
    {
        if (!isShielded)
        {
            currentHealth -= amount;
            UpdateHealth();
            if (currentHealth <= 0)
            {
                anim.SetBool("Death", true);

            }
               
        }
       
    }
   
    public void HealPlayer(float amount)
    {
        
          currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealth();
    }
    

    public void UpdateHealth()
    {
        healthBar.fillAmount = currentHealth / maxHealth;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
            

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            HealPlayer(10);
            

        }

    }


}
