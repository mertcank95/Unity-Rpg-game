using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public float healthAmount = 20f;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //can verme 1.yol
        /*
        player.GetComponent<PlayerHealth>().currentHealth += healthAmount;
        player.GetComponent<PlayerHealth>().UpdateHealth();*/
        //2.yol
        player.GetComponent<PlayerHealth>().HealPlayer(healthAmount);


    }

    
}
